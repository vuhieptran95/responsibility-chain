using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Logging;

namespace Notification.Web.ScheduledJobs
{
    public class MarkProjectsAsMissedDeadline : IScheduledJob
    {
        private readonly ILogger<MarkProjectsAsMissedDeadline> _logger;

        public MarkProjectsAsMissedDeadline(ILogger<MarkProjectsAsMissedDeadline> logger)
        {
            _logger = logger;
        }
        public async Task Execute()
        {
            try
            {
                var missedDeadlineYearWeek = 0;
            
                var phrEndpoint =
                    AppSettings.ExternalServices.PHR.Endpoint
                        .AppendPathSegment("api/projects/missed-deadline-projects")
                        .WithHeader("Content-Type", "application/json");

                await phrEndpoint.PostJsonAsync(new {missedDeadlineYearWeek});
            }
            catch (FlurlHttpException e)
            {
                _logger.LogError(
                    $"{nameof(MarkProjectsAsMissedDeadline)} - {nameof(FlurlHttpException)}: " + "{error}",
                    await e.GetResponseStringAsync());
                throw;
            }
        }
    }
}