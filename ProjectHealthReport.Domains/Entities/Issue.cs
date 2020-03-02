﻿using System.Collections.Generic;

 namespace ProjectHealthReport.Domains.Entities
{
    public class Issue
    {
        public Issue()
        {
            AdditionalInfoIssues = new HashSet<AdditionalInfoIssues>();
        }

        public int Id { get; set; }
        public string Item { get; set; }
        public string Impact { get; set; }
        public string Action { get; set; }
        public int OpenedYearWeek { get; set; }
        public ICollection<AdditionalInfoIssues> AdditionalInfoIssues { get; set; }
    }
}