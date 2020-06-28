using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Domains.Mappings;
using ProjectHealthReport.Features.Projects.Queries.GetProjectsCaching;
using ResponsibilityChain;
using ResponsibilityChain.Business;
using ResponsibilityChain.Business.AuthorizationConfigs;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.Projects.Queries.GetProjectIndexPhr
{
    public partial class GetProjectIndexPhrQuery : Request<GetProjectIndexPhrQuery.Dto>
    {
        public Expression<Func<GetProjectsCachingQuery.Project, bool>> ResourceFilter { get; set; } = p => true;

        public class Hander : IExecution<GetProjectIndexPhrQuery, Dto>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IMediator _mediator;

            public Hander(ReportDbContext dbContext, IMediator mediator)
            {
                _dbContext = dbContext;
                _mediator = mediator;
            }

            public async Task HandleAsync(GetProjectIndexPhrQuery request)
            {
                var dto = new Dto();

                    dto.Projects = (await _mediator.SendAsync(new GetProjectsCachingQuery())).Projects
                        .Where(p => p.PhrRequired)
                        .Where(request.ResourceFilter.Compile())
                        .Select(
                            r => new Dto.Project
                            {
                                Id = r.Id,
                                Name = r.Name,
                                Division = r.Division,
                                KeyAccountManager = r.KeyAccountManager,
                                DeliveryResponsibleName = r.DeliveryResponsibleName,
                                ProjectStartDate = r.ProjectStartDate,
                            }
                        )
                        .OrderBy(p => p.Division)
                        .ToList();

                request.Response = dto;
            }
        }

        public class Dto
        {
            public IEnumerable<Project> Projects { get; set; }

            public class Project : IMapFrom<Domains.Domains.Project>
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public string Division { get; set; }
                public string KeyAccountManager { get; set; }
                public string DeliveryResponsibleName { get; set; }
                public DateTime ProjectStartDate { get; set; }
                public bool IsNotSubmitted { get; set; } = false;
            }
        }

    }
}