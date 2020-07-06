using System;
using System.Net;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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

                var phrEndpoint = (await AppSettings.ExternalServices.PHR.GetFlurlRequest())
                    .AppendPathSegment("phr/missed-deadline/year-week/")
                    .AppendPathSegment(missedDeadlineYearWeek);

                var res = await phrEndpoint.PostAsync(null);
                
                var content = await res.Content.ReadAsStringAsync();

                if (res.StatusCode == HttpStatusCode.Unauthorized)
                {
                    if (string.IsNullOrEmpty(content))
                    {
                        AppSettings.ResetToken();
                        
                        await phrEndpoint.PostAsync(null);
                    }
                    else
                    {
                        var error = JsonConvert.DeserializeObject<ErrorResponse>(content);
                        
                        throw new UnauthorizedAccessException(error.Error);
                    }
                }
            }
            catch (FlurlHttpException e)
            {
                _logger.LogError(
                    $"{nameof(MarkProjectsAsMissedDeadline)} - {nameof(FlurlHttpException)}: " + "{error}",
                    await e.GetResponseStringAsync());
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(
                    $"{nameof(MarkProjectsAsMissedDeadline)} - {nameof(Exception)}: " + "{error}", e.Message);
                throw;
            }
        }
    }
}