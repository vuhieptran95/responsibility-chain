using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectHealthReport.Domains.Entities
{
    public class DivisionFutureResource : IWeeklyReport
    {
        public int Id { get; set; }
        public int YearWeek { get; set; }

        [Required]
        public string Division { get; set; }

        [Required]
        public string Role { get; set; }

        public string Level { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public string Project { get; set; }

        public DateTime Date { get; set; }
        public string Note { get; set; }
    }
}
