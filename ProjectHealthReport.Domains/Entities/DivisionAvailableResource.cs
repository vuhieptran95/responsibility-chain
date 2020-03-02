﻿using System.ComponentModel.DataAnnotations;

 namespace ProjectHealthReport.Domains.Entities
{
    public class DivisionAvailableResource :IWeeklyReport
    {
        public int Id { get; set; }
        public int YearWeek { get; set; }

        [Required]
        public string Division { get; set; }

        [Required]
        public string ResourceEmail { get; set; }

        [Required]
        public int Billable { get; set; }

        [Required]
        public int Nonbillable { get; set; }

        public string Role { get; set; }
        public string Level { get; set; }
        public string Note { get; set; }
    }
}
