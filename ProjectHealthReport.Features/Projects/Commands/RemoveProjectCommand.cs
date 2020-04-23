using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ResponsibilityChain;
using ResponsibilityChain.Business.AuthorizationConfigs;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.Projects.Commands
{
    public class RemoveProjectCommand : Request<int>
    {
        public int ProjectId { get; set; }
        
        public class AuthorizationConfig : IAuthorizationConfig<RemoveProjectCommand>
        {
            public List<(string[] Resources, string[] Actions)> GetAccessRights()
            {
                return new List<(string[] Resources, string[] Actions)>()
                {
                    (new[] {Resources.Project, Resources.ProjectMaster}, new[] {Actions.Read, Actions.Delete}),
                };
            }
        }

        public class Handler : IExecution<RemoveProjectCommand, int>
        {
            private readonly ReportDbContext _dbContext;

            public Handler(ReportDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task HandleAsync(RemoveProjectCommand request)
            {
                var project = new Project();
                
                // Set project Id with Reflection
                project.GetType().GetProperty("_id").SetValue(project, request.ProjectId);

                _dbContext.Projects.Remove(project);
                await _dbContext.SaveChangesAsync();

                request.Response = 1;
            }
        }
    }
}