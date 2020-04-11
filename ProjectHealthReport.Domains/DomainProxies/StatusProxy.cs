using System;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Mappings;

namespace ProjectHealthReport.Domains.DomainProxies
{
    public class StatusProxy : IWeeklyReport, IMapFrom<Status>, IMapTo<Status>
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string StatusColor { get; set; }
        public string ProjectStatus { get; set; }
        public string RetrospectiveFeedBack { get; set; }
        public DateTime? MilestoneDate { get; set; }
        public string Milestone { get; set; }
        public int YearWeek { get; set; }
        public ProjectProxy Project { get; set; }
    }
}