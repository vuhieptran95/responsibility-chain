using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectHealthReport.Domains.Entities
{
    public class DivisionUpdatedResource : IWeeklyReport
    {
        public int Id { get; set; }
        public int YearWeek { get; set; }

        [Required]
        public string Division { get; set; }

        [Required]
        public string ResourceEmail { get; set; }

        [Required]
        public DateTime OnBoardDate { get; set; }

        public string Role { get; set; }
    }
}
