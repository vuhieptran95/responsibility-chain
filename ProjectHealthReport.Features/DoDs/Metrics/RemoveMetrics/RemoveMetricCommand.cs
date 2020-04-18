using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectHealthReport.Domains.Domains;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.DoDs.Metrics.RemoveMetrics
{
    public class RemoveMetricCommand : IRequest<int>
    {
        public int MetricId { get; set; }

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

        public int Response { get; set; }
    }
}