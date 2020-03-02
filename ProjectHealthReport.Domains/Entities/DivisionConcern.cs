﻿using System.ComponentModel.DataAnnotations;

 namespace ProjectHealthReport.Domains.Entities
{
    public class DivisionConcern : IWeeklyReport
    {
        public int Id { get; set; }
        public int YearWeek { get; set; }

        [Required]
        public string Division { get; set; }

        [Required]
        public string Concerns { get; set; }
    }
}
