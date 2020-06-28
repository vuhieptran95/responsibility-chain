using System.IO;
using System.Net;
using System.Net.Mail;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace Notification.Web.Services
{
    public static class MailService
    {
        private static readonly TemplateServiceConfiguration config = new TemplateServiceConfiguration()
            {DisableTempFileLocking = true};

        public static string ProjectNotSubmitWeeklyReportKey = "ProjectNotSubmitWeeklyReportKey";
        public static string ProjectNotSubmitWeeklyReportPath = "./Templates/ProjectsNotYetSubmittedWeeklyReport.cshtml";
        public static string ProjectsNeedDoDKey = "ProjectsNeedDoDkey";
        public static string ProjectsNeedDoDPath = "./Templates/ProjectsNeedDoD.cshtml";

        public static void Register()
        {
            Engine.Razor = RazorEngineService.Create(config);

            CreateTemplate(ProjectNotSubmitWeeklyReportKey, ProjectNotSubmitWeeklyReportPath, new
            {
                ProjectName = "",
                Pic = "",
                YearWeek = "",
                PhrLink = "",
                Deadline = ""
            });
        }

        public static string CreateTemplate(string key, string path, object model)
        {
            if (!Engine.Razor.IsTemplateCached(key, null))
            {
                return Engine.Razor.RunCompile(File.ReadAllText(path), key, null, model);
            }
            else
            {
                return Engine.Razor.Run(key, null, model);
            }
        }

        // TODO provide async-API
        public static void Send(string to, string cc, string subject, string body)
        {
            // TODO dispose `smtpClient`
            // TODO SmtpClient is obsolete. Use MailKit instead. https://docs.microsoft.com/en-us/dotnet/api/system.net.mail.smtpclient?view=netframework-4.8
            using var smtpClient = new SmtpClient()
            {
                Host = AppSettings.MailSettings.HostName,
                Port = AppSettings.MailSettings.Port,
                Credentials =
                    new NetworkCredential(AppSettings.MailSettings.Username, AppSettings.MailSettings.Password),
                EnableSsl = AppSettings.MailSettings.EnableSsl
            };
            using var message = new MailMessage();
            message.From = new MailAddress(AppSettings.MailSettings.Mail, "Report System");
            message.To.Add(to);
            message.CC.Add(cc);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            smtpClient.Send(message);
        }
    }
}