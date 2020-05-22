using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProjectHealthReport.Domains.DomainProxies;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Domains.Mappings;
using ProjectHealthReport.Features.Helpers;
using ProjectHealthReport.Features.Projects.Queries.GetProjectCaching;
using ProjectHealthReport.Features.WeeklyReports.Queries.GetWeeklyReportPhr;
using ResponsibilityChain.Business;
using ResponsibilityChain.Business.AuthorizationConfigs;
using ResponsibilityChain.Business.Caching;
using ResponsibilityChain.Business.Events;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.Projects.Commands
{
    public partial class EditProjectNonMasterDataCommand : Request<int>, IMapTo<ProjectProxy>
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
                var userRoleList = await _mediator.SendAsync(new GetListUserRoleQuery());
                
                var projectInDb = await _dbContext.Projects
                    .Include(p => p.BacklogItems)
                    .Include(p => p.ProjectAccesses)
                    .Where(p => p.Id == request.Id)
                    .FirstAsync();

                var backlogItem = new BacklogItem(request.BacklogItem.Id, request.Id, 0,
                    request.BacklogItem.ItemsAdded, request.BacklogItem.StoryPointsAdded, 0, 0, 202000);

                var projectAccesses = request.ProjectAccesses
                    .Select(pa => new ProjectAccess(pa.Id, request.Id, pa.Email, pa.Role, userRoleList)).ToList();

                projectInDb.EditNonMasterData(request.PlatformVersion, request.JIRALink, request.SourceCodeLink,
                    request.Note, request.ProjectEndDate, backlogItem, projectAccesses);

                await _dbContext.SaveChangesAsync();

                request.Response = request.Id;
            }
        }
    }
}