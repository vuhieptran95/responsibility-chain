using System;
using Hangfire;
using Notification.Web.ScheduledJobs;

namespace Notification.Web
{
    public class Jobs
    {
        private readonly NotifyProjectsNotYetSubmittedWeeklyReport _notifyProjectsNotYetSubmittedWeeklyReport;
        private readonly MarkProjectsAsMissedDeadline _markProjectsAsMissedDeadline;
        private readonly NotifyProjectsToDoDoDThisWeek _notifyProjectsToDoDoDThisWeek;

        public Jobs(NotifyProjectsNotYetSubmittedWeeklyReport notifyProjectsNotYetSubmittedWeeklyReport,
            MarkProjectsAsMissedDeadline markProjectsAsMissedDeadline, NotifyProjectsToDoDoDThisWeek notifyProjectsToDoDoDThisWeek)
        {
            _notifyProjectsNotYetSubmittedWeeklyReport = notifyProjectsNotYetSubmittedWeeklyReport;
            _markProjectsAsMissedDeadline = markProjectsAsMissedDeadline;
            _notifyProjectsToDoDoDThisWeek = notifyProjectsToDoDoDThisWeek;
        }
        public void ScheduledJobs()
        {
            RecurringJob.AddOrUpdate(() => _notifyProjectsNotYetSubmittedWeeklyReport.Execute(),
                AppSettings.JobSchedules.NotifyProjectsNotYetSubmittedWeeklyReport.Cron, TimeZoneInfo.Local);

            RecurringJob.AddOrUpdate(() => _notifyProjectsNotYetSubmittedWeeklyReport.Test(),
                AppSettings.JobSchedules.NotifyProjectsNotYetSubmittedWeeklyReport.Cron, TimeZoneInfo.Local);
            
            
            RecurringJob.AddOrUpdate(() => _markProjectsAsMissedDeadline.Execute(),
                AppSettings.JobSchedules.MarkProjectsAsMissedDeadline.Cron, TimeZoneInfo.Local);
            
            RecurringJob.AddOrUpdate(() => _notifyProjectsToDoDoDThisWeek.Execute(),
                AppSettings.JobSchedules.NotifyProjectsToDoDoDThisWeek.Cron, TimeZoneInfo.Local);
        }
    }
}