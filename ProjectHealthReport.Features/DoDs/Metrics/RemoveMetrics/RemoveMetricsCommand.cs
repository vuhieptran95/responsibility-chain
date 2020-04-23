using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ResponsibilityChain;
using ResponsibilityChain.Business.AuthorizationConfigs;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.DoDs.Metrics.RemoveMetrics
{
    public class RemoveMetricsCommand : Request<int>
    {
        public string Tool { get; set; }
        
        public class AuthorizationConfig : IAuthorizationConfig<RemoveMetricsCommand>
        {
            public List<(string[] Resources, string[] Actions)> GetAccessRights()
            {
                return new List<(string[] Resources, string[] Actions)>()
                {
                    (new[] {Resources.Metrics}, new[] {Actions.Delete}),
                };
            }
        }

        public class RemoveMetricsCommandHandler : IExecution<RemoveMetricsCommand, int>
        {
            private readonly ReportDbContext _dbContext;

            public RemoveMetricsCommandHandler(ReportDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task HandleAsync(RemoveMetricsCommand request)
            {
                var metricsToRemove = _dbContext.Metrics.Where(m => m.Tool == request.Tool);
                _dbContext.Metrics.RemoveRange(metricsToRemove);
                await _dbContext.SaveChangesAsync();

                request.Response = 1;
            }
        }

    }
}