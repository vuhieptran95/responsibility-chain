using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Notification.Web.ScheduledJobs
{
    public class NotifyProjectsToDoDoDThisWeek : IScheduledJob
    {
        private readonly ILogger<NotifyProjectsToDoDoDThisWeek> _logger;

        public NotifyProjectsToDoDoDThisWeek(ILogger<NotifyProjectsToDoDoDThisWeek> logger)
        {
            _logger = logger;
        }

        public async Task Execute()
        {
            var subject = "[Project Health Report] Reminder to create weekly reports before deadline";
            var response = await CallApi();
            
            foreach (var project in response.Projects)
            {
                MailService.Send(project.PicEmail, project.DmEmail + "," + AppSettings.MailSettings.MailList.Pmo + project.PicAlikeEmailsString,
                    subject, project.HtmlContent);
            }
        }

        private async Task<Dto> CallApi()
        {
            // var currentYearWeek = TimeHelper.GetYearWeek(DateTime.Now);
            var currentYearWeek = 202015;
            var firstWorkingDate = TimeHelper.GetFirstWorkingDateOfWeek(currentYearWeek);
            var lastWorkingDate = TimeHelper.GetLastWorkingDateOfWeek(firstWorkingDate);
            var workingSpan = $"({firstWorkingDate.ToString("M")} - {lastWorkingDate.ToString("M")})";

            var phrEndpoint =
                AppSettings.ExternalServices.PHR.Endpoint
                    .AppendPathSegment("api/projects/require-dod/year-week")
                    .AppendPathSegment(currentYearWeek)
                    .WithHeader("Content-Type", "application/json");

            try
            {
                var response = await phrEndpoint
                    .GetJsonAsync<Dto>();
                foreach (var project in response.Projects)
                {
                    var htmlContent = MailService.CreateTemplate(MailService.ProjectsNeedDoDKey,
                        MailService.ProjectsNeedDoDPath, new
                        {
                            ProjectName = project.Name,
                            Time = workingSpan,
                            Pic = project.PicEmail.TrimEnd("niteco.se".ToCharArray()).TrimEnd('@'),
                            YearWeek = TimeHelper.CalculateWeek(project.SelectedYearWeek) + " - " +
                                       TimeHelper.CalculateYear(project.SelectedYearWeek),
                            PhrLink = AppSettings.ExternalServices.PHR.Endpoint,
                        });

                    project.HtmlContent = htmlContent;
                }

                _logger.LogInformation(nameof(NotifyProjectsToDoDoDThisWeek) + " PHR response: {@response}", response);

                return response;
            }
            catch (FlurlHttpException e)
            {
                _logger.LogError(
                    $"{nameof(NotifyProjectsToDoDoDThisWeek)} - {nameof(FlurlHttpException)}: " + "{error}",
                    await e.GetResponseStringAsync());
                throw;
            }
        }

        public class Dto
        {
            public IEnumerable<ProjectDto> Projects { get; set; }
        }

        public class ProjectDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int SelectedYearWeek { get; set; }
            public string PicEmail { get; set; }
            public List<string> PicAlikeEmails { get; set; }
            public string DmEmail { get; set; }
            public string DivisionName { get; set; }
            public string HtmlContent { get; set; }

            public string PicAlikeEmailsString
            {
                get
                {
                    if (PicAlikeEmails == null || PicAlikeEmails.Count < 1)
                    {
                        return "";
                    }

                    if (PicAlikeEmails.Count == 1)
                    {
                        return "," + PicAlikeEmails[0];
                    }

                    return "," + string.Join(",", PicAlikeEmails);
                }
            }
        }
    }
}