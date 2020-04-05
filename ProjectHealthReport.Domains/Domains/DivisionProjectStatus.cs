using System.ComponentModel.DataAnnotations;

namespace ProjectHealthReport.Domains.Domains
{
    public class DivisionProjectStatus : IWeeklyReport
    {
        public int Id { get; set; }
        public int YearWeek { get; set; }
        public int ProjectId { get; set; }

        [Required]
        public string StatusColor { get; set; }

        public string ProjectStatus { get; set; }
        public string Actions { get; set; }
        public Project Project { get; set; }
    }
}
