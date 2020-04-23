using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Mappings;
using ProjectHealthReport.Features.Common;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.Projects.Queries.GetProjects
{
    public partial class GetProjectsQuery : Request<GetProjectsQuery.Dto>
    {
        public Expression<Func<Domains.Domains.Project, bool>> ResourceFilter { get; set; } = p => true;
    }

    public partial class GetProjectsQuery
    {
        public class Handler : IExecution<GetProjectsQuery, Dto>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IMapper _mapper;

            public Handler(ReportDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public async Task HandleAsync(GetProjectsQuery request)
            {
                var projects = await _dbContext.Projects
                    .Where(request.ResourceFilter)
                    .OrderByDescending(p => p.Id)
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
    }
}