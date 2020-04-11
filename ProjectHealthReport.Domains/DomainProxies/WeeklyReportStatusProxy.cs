using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Mappings;

namespace ProjectHealthReport.Domains.DomainProxies
{
    public class WeeklyReportStatusProxy : IMapFrom<WeeklyReportStatus>, IMapTo<WeeklyReportStatus>
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int YearWeek { get; set; }
        public bool? IsDeadlineMissed { get; set; }
        public ProjectProxy Project { get; set; }
    }
}