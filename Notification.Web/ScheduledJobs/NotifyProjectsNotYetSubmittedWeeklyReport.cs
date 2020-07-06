using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Notification.Web.Services;
using ProjectHealthReport.Domains.Helpers;

namespace Notification.Web.ScheduledJobs
{
    public class TokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("expires_in")]
        public string ExpireIn { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("scope")]
        public string Scope { get; set; }
    }
    public class ErrorResponse
    {
        public ErrorResponse(string error, HttpStatusCode httpStatusCode, object info)
        {
            Error = error;
            HttpStatusCode = httpStatusCode;
            Info = info;
        }

        public string Error { get; }
        public HttpStatusCode HttpStatusCode { get; }
        public object Info { get; set; }
    }
    public class NotifyProjectsNotYetSubmittedWeeklyReport : IScheduledJob
    {
        private readonly ILogger<NotifyProjectsNotYetSubmittedWeeklyReport> _logger;

        public NotifyProjectsNotYetSubmittedWeeklyReport(ILogger<NotifyProjectsNotYetSubmittedWeeklyReport> logger)
        {
            _logger = logger;
        }

        // TODO add `Async` suffix
        public async Task Execute()
        {
            var subject = "[Project Health Report] Reminder to create weekly reports before deadline";
            var response = await CallApi();
            
            _logger.LogDebug("NotifyProjectsNotYetSubmittedWeeklyReport: @response", response);
            
            foreach (var project in response.Projects)
            {
               MailService.Send(project.PicEmail, AppSettings.MailSettings.MailList.Pmo, subject, project.HtmlContent);
            }
        }

        private async Task<ProjectsNotYetSubmittedWeeklyReport> CallApi()
        {
            var currentYearWeek = TimeHelper.GetYearWeek(DateTime.Now);
            var lastYearWeek = TimeHelper.GetLastYearWeek(currentYearWeek);
            var phrEndpoint = (await
                AppSettings.ExternalServices.PHR.GetFlurlRequest())
                    .AppendPathSegment("/api/projects/phr/not-submit-report/year-week/")
                    .AppendPathSegment(lastYearWeek);

            try
            {
                var res = await phrEndpoint.AllowHttpStatus(HttpStatusCode.Unauthorized)
                    .GetAsync();
                var content = await res.Content.ReadAsStringAsync();

                if (res.StatusCode == HttpStatusCode.Unauthorized)
                {
                    if (string.IsNullOrEmpty(content))
                    {
                        AppSettings.ResetToken();
                        
                        res = await phrEndpoint.GetAsync();
                        content = await res.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        var error = JsonConvert.DeserializeObject<ErrorResponse>(content);
                        
                        throw new UnauthorizedAccessException(error.Error);
                    }
                }

                var response = JsonConvert.DeserializeObject<ProjectsNotYetSubmittedWeeklyReport>(content);

                foreach (var project in response.Projects)
                {
                    var htmlContent = MailService.CreateTemplate(MailService.ProjectNotSubmitWeeklyReportKey,
                        MailService.ProjectNotSubmitWeeklyReportPath, new
                        {
                            project.ProjectName,
                            Deadline = response.Deadline.ToString("HH:mm dd MMM yyyy"),
                            Pic = project.PicEmail.TrimEnd("niteco.se".ToCharArray()).TrimEnd('@'),
                            YearWeek = TimeHelper.CalculateWeek(project.SelectedYearWeek) + " - " +
                                       TimeHelper.CalculateYear(project.SelectedYearWeek),
                            PhrLink = AppSettings.ExternalServices.PHR.Endpoint,
                        });

                    project.HtmlContent = htmlContent;
                }

                _logger.LogInformation(nameof(NotifyProjectsNotYetSubmittedWeeklyReport) + " PHR response: {@response}",
                    response);

                return response;
            }
            catch (FlurlHttpException e)
            {
                _logger.LogError(
                    $"{nameof(ProjectsNotYetSubmittedWeeklyReport)} - {nameof(FlurlHttpException)}: " + "{error}",
                    await e.GetResponseStringAsync());
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(
                    $"{nameof(ProjectsNotYetSubmittedWeeklyReport)} - {nameof(Exception)}: " + "{error}", e.Message);
                throw;
            }
        }

        public class ProjectsNotYetSubmittedWeeklyReport
        {
            public Project[] Projects { get; set; }
            public DateTime Deadline { get; set; }
        }

        public class Project
        {
            public long ProjectId { get; set; }
            public string ProjectName { get; set; }
            public int SelectedYearWeek { get; set; }
            public string PicEmail { get; set; }
            public string DmEmail { get; set; }
            public string DivisionName { get; set; }
            public string HtmlContent { get; set; }
        }
    }
}