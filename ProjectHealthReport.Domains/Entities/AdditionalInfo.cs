﻿using System.Collections.Generic;

 namespace ProjectHealthReport.Domains.Entities
{
    public class AdditionalInfo : IWeeklyReport
    {
        public AdditionalInfo()
        {
            AdditionalInfoIssues = new HashSet<AdditionalInfoIssues>();
        }

        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int YearWeek { get; set; }
        public Project Project { get; set; }
        public ICollection<AdditionalInfoIssues> AdditionalInfoIssues { get; set; }
    }
}