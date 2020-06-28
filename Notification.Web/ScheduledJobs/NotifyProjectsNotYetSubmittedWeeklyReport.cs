using System;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using Notification.Web.Services;
using ProjectHealthReport.Domains.Helpers;

namespace Notification.Web.ScheduledJobs
{
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

            foreach (var project in response.Projects)
            {
               MailService.Send(project.PicEmail, project.DmEmail + "," + AppSettings.MailSettings.MailList.Pmo, subject, project.HtmlContent);
            }
        }

        public async Task Test()
        {
            var subject = "[Project Health Report] Reminder to create weekly reports before deadline";
            var response = await CallApi();

            response.Projects = response.Projects.Take(2).ToArray();

            foreach (var project in response.Projects)
            {
                project.PicEmail = "hiep.tran2@niteco.se";
                project.DmEmail = "hiep.tran2@niteco.se";
                MailService.Send(project.PicEmail, project.DmEmail + "," + "hiep.tran2@niteco.se", subject, project.HtmlContent);
            }
        }

        private async Task<ProjectsNotYetSubmittedWeeklyReport> CallApi()
        {
            var currentYearWeek = TimeHelper.GetYearWeek(DateTime.Now);
            var lastYearWeek = TimeHelper.GetLastYearWeek(currentYearWeek);
            var phrEndpoint =
                AppSettings.ExternalServices.PHR.Endpoint
                    .AppendPathSegment("api/projects/not-submit-report-projects/year-week")
                    .AppendPathSegment(lastYearWeek)
                    .WithHeader("Content-Type", "application/json");

            try
            {
                var response = await phrEndpoint
                    .GetJsonAsync<ProjectsNotYetSubmittedWeeklyReport>();
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
            
                _logger.LogInformation(nameof(NotifyProjectsNotYetSubmittedWeeklyReport) + " PHR response: {@response}", response);

                return response;
            }
            catch (FlurlHttpException e)
            {
                _logger.LogError(
                    $"{nameof(ProjectsNotYetSubmittedWeeklyReport)} - {nameof(FlurlHttpException)}: " + "{error}",
                    await e.GetResponseStringAsync());
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