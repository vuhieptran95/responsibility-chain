using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectHealthReport.Domains.Domains;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.Helpers
{
    public class GetProjectStateTypesQuery : IRequest<GetProjectStateTypesQuery.Dto>
    {
        public class Handler : ExecutionHandlerBase<GetProjectStateTypesQuery, Dto>
        {
            private readonly ReportDbContext _dbContext;

            public Handler(ReportDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public override async Task HandleAsync(GetProjectStateTypesQuery request)
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

        public Dto Response { get; set; }
    }
}