using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectHealthReport.Domains.Domains
{
    public class Status : IWeeklyReport
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        [Required]
        public string StatusColor { get; set; }
        public string ProjectStatus { get; set; }
        public string RetrospectiveFeedBack { get; set; }
        public DateTime? MilestoneDate { get; set; }
        public string Milestone { get; set; }
        public int YearWeek { get; set; }
        public Project Project { get; set; }
    }
}