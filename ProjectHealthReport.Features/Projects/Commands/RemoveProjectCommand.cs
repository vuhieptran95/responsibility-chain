using System.Threading.Tasks;
using ProjectHealthReport.Domains.Domains;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.Projects.Commands
{
    public class RemoveProjectCommand : IRequest<int>
    {
        public int ProjectId { get; set; }

        public class Handler : ExecutionHandlerBase<RemoveProjectCommand, int>
        {
            private readonly ReportDbContext _dbContext;

            public Handler(ReportDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public override async Task HandleAsync(RemoveProjectCommand request)
            {
                var project = new Project();
                
                // Set project Id with Reflection
                project.GetType().GetProperty("_id").SetValue(project, request.ProjectId);

                _dbContext.Projects.Remove(project);
                await _dbContext.SaveChangesAsync();

                request.Response = 1;
            }
        }

        public int Response { get; set; }
    }
}