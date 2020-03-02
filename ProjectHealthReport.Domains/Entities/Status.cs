﻿using System.ComponentModel.DataAnnotations;

 namespace ProjectHealthReport.Domains.Entities
{
    public class Status : IWeeklyReport
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }

        [Required]
        public string StatusColor { get; set; }

        public string ProjectStatus { get; set; }
        public string RetrospectiveFeedBack { get; set; }
        public int YearWeek { get; set; }
        public Project Project { get; set; }
    }
}