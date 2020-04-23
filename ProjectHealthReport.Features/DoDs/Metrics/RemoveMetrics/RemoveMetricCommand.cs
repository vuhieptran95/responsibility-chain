using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ResponsibilityChain;
using ResponsibilityChain.Business.AuthorizationConfigs;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.DoDs.Metrics.RemoveMetrics
{
    public class RemoveMetricCommand : Request<int>
    {
        public int MetricId { get; set; }
        
        public class AuthorizationConfig : IAuthorizationConfig<RemoveMetricCommand>
        {
            public List<(string[] Resources, string[] Actions)> GetAccessRights()
            {
                return new List<(string[] Resources, string[] Actions)>()
                {
                    (new[] {Resources.Metrics}, new[] {Actions.Delete}),
                };
            }
        }

        public class Handler : IExecution<RemoveMetricCommand, int>
        {
            private readonly ReportDbContext _dbContext;

            public Handler(ReportDbContext dbContext)
            {
                _dbContext = dbContext;
            }


            public async Task HandleAsync(RemoveMetricCommand request)
            {
                var metric = await _dbContext.Metrics.FirstAsync(m => m.Id == request.MetricId);
                _dbContext.Metrics.Remove(metric);
                await _dbContext.SaveChangesAsync();

                request.Response = 1;
            }
        }

    }
}