using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectHealthReport.Domains.Domains;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.Helpers
{
    public class GetProjectStateTypesQuery : Request<GetProjectStateTypesQuery.Dto>
    {
        public class Handler : IExecution<GetProjectStateTypesQuery, Dto>
        {
            private readonly ReportDbContext _dbContext;

            public Handler(ReportDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task HandleAsync(GetProjectStateTypesQuery request)
            {
                var stateTypes = await _dbContext.ProjectStateTypes.Select(s =>
                    new Dto.ProjectStateType()
                    {
                        Id = s.Id,
                        State = s.State
                    }).ToListAsync();

                request.Response = new Dto() {StateTypes = stateTypes};
            }
        }

        public class Dto
        {
            public IEnumerable<ProjectStateType> StateTypes { get; set; }

            public class ProjectStateType
            {
                public int Id { get; set; }
                public string State { get; set; }
            }
        }

    }
}