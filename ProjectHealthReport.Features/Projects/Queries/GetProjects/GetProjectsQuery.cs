using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Mappings;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.Projects.Queries.GetProjects
{
    public class GetProjectsQuery : IRequest<GetProjectsQuery.Dto>
    {
        public class Handler : ExecutionHandler<GetProjectsQuery, Dto>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IMapper _mapper;

            public Handler(ReportDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public override async Task HandleAsync(GetProjectsQuery request)
            {
                var projects = await _dbContext.Projects.OrderByDescending(p => p.Id)
                    .ProjectTo<Dto.ProjectDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                projects.ForEach(p =>
                {
                    p.ProjectStateType = p.ProjectStateType.ToLower();
                    p.DeliveryResponsibleName = p.DeliveryResponsibleName?.TrimEnd("niteco.se".ToCharArray())
                        .TrimEnd("niteco.com".ToCharArray()).TrimEnd('@');
                    p.KeyAccountManager = p.KeyAccountManager?.TrimEnd("niteco.se".ToCharArray())
                        .TrimEnd("niteco.com".ToCharArray()).TrimEnd('@');
                });

                request.Response = new Dto(projects);
            }
        }

        public class Dto
        {
            public Dto(IEnumerable<ProjectDto> projects)
            {
                Projects = projects;
            }

            public IEnumerable<ProjectDto> Projects { get; set; }

            public class ProjectDto : IMapFrom<Project>
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public string Code { get; set; }
                public string Division { get; set; }
                public string KeyAccountManager { get; set; }
                public string DeliveryResponsibleName { get; set; }
                public bool PhrRequired { get; set; }
                public bool DodRequired { get; set; }
                public bool DmrRequired { get; set; }
                public DateTime? DmrRequiredFrom { get; set; }
                public DateTime? DmrRequiredTo { get; set; }
                public string ProjectStateType { get; set; }
                public int ProjectStateTypeId { get; set; }

                public void MappingFrom(Profile profile)
                {
                    profile.CreateMap<Domains.Domains.Project, ProjectDto>()
                        .ForMember(des =>
                            des.ProjectStateType, opt =>
                            opt.MapFrom(src => src.ProjectStateType.State));
                }
            }
        }

        public Dto Response { get; set; }
    }
}