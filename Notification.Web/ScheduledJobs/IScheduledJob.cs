﻿using System.Threading.Tasks;

 namespace Notification.Web.ScheduledJobs
{
    public interface IScheduledJob
    {
        Task Execute();
    }
}