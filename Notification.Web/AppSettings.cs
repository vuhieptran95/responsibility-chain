using System.Diagnostics.CodeAnalysis;

namespace Notification.Web
{
    [SuppressMessage("ReSharper", "RCS1102")]
    public class AppSettings
    {
        public static Logging Logging { get; set; }
        public static ConnectionStrings ConnectionStrings { get; set; }
        public static string AllowedHosts { get; set; }
        public static JobSchedules JobSchedules { get; set; }
        public static MailSettings MailSettings { get; set; }
        public static ExternalServices ExternalServices { get; set; }
    }

    public class ConnectionStrings
    {
        public string HangfireConnection { get; set; }
    }

    public class JobSchedules
    {
        public ScheduleJob NotifyProjectsNotYetSubmittedWeeklyReport { get; set; }
        public ScheduleJob MarkProjectsAsMissedDeadline { get; set; }
        public ScheduleJob NotifyProjectsToDoDoDThisWeek { get; set; }
    }

    public class Logging
    {
        public LogLevel LogLevel { get; set; }
    }

    public class LogLevel
    {
        public string Default { get; set; }
        public string Microsoft { get; set; }
        public string MicrosoftHostingLifetime { get; set; }
    }

    public class MailSettings
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string Mail { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public MailList MailList { get; set; }
    }

    public class MailList
    {
        public string Pmo { get; set; }
        public string Coo { get; set; }
    }

    public class ScheduleJob
    {
        public string Cron { get; set; }
    }

    public class ExternalServices
    {
        public ExternalService PHR { get; set; }
        public ExternalService IdP { get; set; }
    }

    public class ExternalService
    {
        public string Endpoint { get; set; }
        public string AuthorizationHeader { get; set; }
    }
}