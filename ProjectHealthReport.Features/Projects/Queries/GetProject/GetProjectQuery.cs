using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Domains.Mappings;
using ProjectHealthReport.Features.Helpers;
using ProjectHealthReport.Features.Projects.Queries.GetProjectCaching;
using ResponsibilityChain;
using ResponsibilityChain.Business;
using ResponsibilityChain.Business.AuthorizationConfigs;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.Processors;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.Projects.Queries.GetProjects
{
    public partial class GetProjectQuery : Request<GetProjectQuery.Dto>
    {
        public class AuthorizationConfig: IAuthorizationConfig<GetProjectQuery>
        {
            public List<(string[] Resources, string[] Actions)> GetAccessRights()
            {
                return new List<(string[] Resources, string[] Actions)>()
                {
                    (new[] {Resources.Project, Resources.BacklogItem, Resources.ProjectAccess}, new[] {Actions.Read}),
                };
            }
        }
        public int ProjectId { get; set; }

        public class Handler : IExecution<GetProjectQuery, Dto>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IMapper _mapper;

            public Handler(ReportDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public async Task HandleAsync(GetProjectQuery request)
            {
                var dto = await _dbContext.Projects
                    .Where(p => p.Id == request.ProjectId)
                    .ProjectTo<Dto>(_mapper.ConfigurationProvider)
                    .FirstAsync();

                var backlogItem = await _dbContext.BacklogItems
                        .Where(b => b.ProjectId == request.ProjectId && (b.YearWeek % 100) == 0)
                        .ProjectTo<Dto.BacklogItemDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();

                dto.BacklogItem = backlogItem;
                
                request.Response = dto;
            }
        }
        
        public class GetUserEmailsProcessor: IPostProcessor<GetProjectQuery, Dto>
        {
            private readonly IMediator _mediator;

            public GetUserEmailsProcessor(IMediator mediator)
            {
                _mediator = mediator;
            }
            public async Task HandleAsync(GetProjectQuery request)
            {
                var userRoleList = await _mediator.SendAsync(new GetListUserRoleQuery());
                request.Response.UserEmails = userRoleList.Select(i => i.Email);
            }
        }


        public class Dto : IMapFrom<Project>
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
            public string Division { get; set; }
            public bool PhrRequired { get; set; }
            public bool DmrRequired { get; set; }
            public bool DodRequired { get; set; }
            public DateTime? DmrRequiredFrom { get; set; }
            public DateTime? DmrRequiredTo { get; set; }
            public int ProjectStateTypeId { get; set; }
            public string ProjectState { get; set; }
            public string KeyAccountManager { get; set; }
            public string DeliveryResponsibleName { get; set; }
            public BacklogItemDto BacklogItem { get; set; }
            public IEnumerable<ProjectAccessDto> ProjectAccesses { get; set; }
            public string PlatformVersion { get; set; }

            public string JIRALink { get; set; }
            public string SourceCodeLink { get; set; }
            public DateTime ProjectStartDate { get; set; }
            public DateTime? ProjectEndDate { get; set; }
            public string Note { get; set; }
            public IEnumerable<string> UserEmails { get; set; }

            public class BacklogItemDto : IMapFrom<BacklogItem>
            {
                public int Id { get; set; }
                public int ItemsAdded { get; set; }
                public int? StoryPointsAdded { get; set; }
            }
            
            public class ProjectAccessDto : IMapFrom<ProjectAccess>
            {
                public int Id { get; set; }
                public int ProjectId { get; set; }
                public string Email { get; set; }
                public string Role { get; set; }
            }

            public void MappingFrom(Profile profile)
            {
                profile.CreateMap<Project, Dto>()
                    .ForMember(des => des.ProjectState, opt => opt.MapFrom(src => src.ProjectStateType.State));
            }
        }
    }
}