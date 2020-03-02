using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectHealthReport.Domains.Entities
{
    public class DivisionSoonAvailableResource : IWeeklyReport
    {
        public int Id { get; set; }
        public int YearWeek { get; set; }

        [Required]
        public string Division { get; set; }

        [Required]
        public string ResourceEmail { get; set; }

        [Required]
        public int Availability { get; set; }

        [Required]
        public DateTime StartingAvailableDate { get; set; }

        public string Role { get; set; }
        public string Level { get; set; }
        public string Note { get; set; }
    }
}
