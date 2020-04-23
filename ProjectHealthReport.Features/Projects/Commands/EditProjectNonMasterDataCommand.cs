using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectHealthReport.Domains.DomainProxies;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Domains.Mappings;
using ProjectHealthReport.Features.Helpers;
using ResponsibilityChain.Business;
using ResponsibilityChain.Business.AuthorizationConfigs;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.Projects.Commands
{
    public class EditProjectNonMasterDataCommand : Request<int>, IMapTo<ProjectProxy>
    {
        public int Id { get; set; }
        public BacklogItemDto BacklogItem { get; set; }

        public string PlatformVersion { get; set; }

        public string JIRALink { get; set; }

        public string SourceCodeLink { get; set; }

        public string Note { get; set; }

        public DateTime? ProjectEndDate { get; set; }
        public IEnumerable<ProjectAccessDto> ProjectAccesses { get; set; }

        public class BacklogItemDto : IMapFrom<BacklogItemProxy>
        {
            public int Id { get; set; }
            public int ItemsAdded { get; set; }
            public int? StoryPointsAdded { get; set; }
        }

        public class ProjectAccessDto : IMapTo<ProjectAccessProxy>
        {
            public int Id { get; set; }
            public int ProjectId { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
        }
        
        public class AuthorizationConfig : IAuthorizationConfig<EditProjectMasterDataCommand>
        {
            public List<(string[] Resources, string[] Actions)> GetAccessRights()
            {
                return new List<(string[] Resources, string[] Actions)>()
                {
                    (new[] {Resources.Project, Resources.ProjectNonMaster}, new[] {Actions.Read, Actions.Create, Actions.Update}),
                };
            }
        }

        public class Handler : IExecution<EditProjectNonMasterDataCommand, int>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public Handler(ReportDbContext dbContext, IMapper mapper, IMediator mediator)
            {
                _dbContext = dbContext;
                _mapper = mapper;
                _mediator = mediator;
            }

            public async Task HandleAsync(EditProjectNonMasterDataCommand request)
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var userRoleList = await _mediator.SendAsync(new GetListUserRoleQuery());
                        var projectInDb = await _dbContext.Projects.AsNoTracking().Include(p => p.ProjectAccesses)
                            .SingleAsync(p => p.Id == request.Id);
                        var projectProxy = _mapper.Map<ProjectProxy>(projectInDb);
                        _mapper.Map(request, projectProxy);

                        _dbContext.Update(_mapper.Map<Project>(projectProxy, opt =>
                            opt.Items[MiscHelper.UserRoleListCtor] = userRoleList));

                        await _dbContext.SaveChangesAsync();

                        var backlogItemInDb =
                            await _dbContext.BacklogItems.SingleOrDefaultAsync(b => b.Id == request.BacklogItem.Id);
                        var backlogItemProxy = _mapper.Map<BacklogItemProxy>(backlogItemInDb);

                        if (backlogItemProxy == null)
                        {
                            var backlogItem = new BacklogItem(0, request.Id, 0, request.BacklogItem.ItemsAdded,
                                request.BacklogItem.StoryPointsAdded, 0, 0,
                                TimeHelper.CalculateYearWeek(DateTime.Now.Year, 0), null);

                            await _dbContext.BacklogItems.AddAsync(backlogItem);
                            await _dbContext.SaveChangesAsync();
                        }
                        else
                        {
                            backlogItemProxy.ItemsAdded = request.BacklogItem.ItemsAdded;
                            backlogItemProxy.StoryPointsAdded = request.BacklogItem.StoryPointsAdded;
                            backlogItemProxy.StoryPointsDone = 0;

                            _dbContext.BacklogItems.Update(_mapper.Map<BacklogItem>(backlogItemProxy));
                            await _dbContext.SaveChangesAsync();
                        }

                        await transaction.CommitAsync();

                        request.Response = request.Id;
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
        }
    }
}