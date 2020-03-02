﻿namespace ProjectHealthReport.Domains.Entities
{
    public class WeeklyReportStatus
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int YearWeek { get; set; }
        public bool? IsDeadlineMissed { get; set; }
        public Project Project { get; set; }
    }
}