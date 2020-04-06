using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Features.Common.Mappings;
using ProjectHealthReport.Features.Projects.Queries.GetProjects;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.Projects.Queries.GetProject
{
    public class GetProjectQuery : IRequest<GetProjectQuery.Dto>
    {
        public int ProjectId { get; set; }

        public class Handler : ExecutionHandlerBase<GetProjectQuery, Dto>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IMapper _mapper;

            public Handler(ReportDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public override async Task<Dto> HandleAsync(GetProjectQuery request)
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
                
                return dto;
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
        }
    }
}