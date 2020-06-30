using System;
using Hangfire;
using Notification.Web.ScheduledJobs;

namespace Notification.Web
{
    public class Jobs
    {
        private readonly NotifyProjectsNotYetSubmittedWeeklyReport _notifyProjectsNotYetSubmittedWeeklyReport;
        private readonly MarkProjectsAsMissedDeadline _markProjectsAsMissedDeadline;

        public Jobs(NotifyProjectsNotYetSubmittedWeeklyReport notifyProjectsNotYetSubmittedWeeklyReport,
            MarkProjectsAsMissedDeadline markProjectsAsMissedDeadline)
        {
            _notifyProjectsNotYetSubmittedWeeklyReport = notifyProjectsNotYetSubmittedWeeklyReport;
            _markProjectsAsMissedDeadline = markProjectsAsMissedDeadline;
        }
        public void ScheduledJobs()
        {
            RecurringJob.AddOrUpdate(() => _notifyProjectsNotYetSubmittedWeeklyReport.Execute(),
                AppSettings.JobSchedules.NotifyProjectsNotYetSubmittedWeeklyReport.Cron, TimeZoneInfo.Local);
            
            RecurringJob.AddOrUpdate(() => _markProjectsAsMissedDeadline.Execute(),
                AppSettings.JobSchedules.MarkProjectsAsMissedDeadline.Cron, TimeZoneInfo.Local);
        }
    }
}