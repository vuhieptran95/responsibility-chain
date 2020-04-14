using System.Linq;
using System.Threading.Tasks;
using ProjectHealthReport.Domains.Domains;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.DoDs.Metrics.RemoveMetrics
{
    public class RemoveMetricsCommand : IRequest<int>
    {
        public string Tool { get; set; }

        public class RemoveMetricsCommandHandler : ExecutionHandlerBase<RemoveMetricsCommand, int>
        {
            private readonly ReportDbContext _dbContext;

            public RemoveMetricsCommandHandler(ReportDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public override async Task HandleAsync(RemoveMetricsCommand request)
            {
                var metricsToRemove = _dbContext.Metrics.Where(m => m.Tool == request.Tool);
                _dbContext.Metrics.RemoveRange(metricsToRemove);
                await _dbContext.SaveChangesAsync();

                request.Response = 1;
            }
        }

        public int Response { get; set; }
    }
}