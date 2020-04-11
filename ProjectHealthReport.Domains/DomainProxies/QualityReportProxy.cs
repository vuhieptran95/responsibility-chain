using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Mappings;

namespace ProjectHealthReport.Domains.DomainProxies
{
    public class QualityReportProxy : IWeeklyReport, IMapFrom<QualityReport>, IMapTo<QualityReport>
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int CriticalBugs { get; set; }
        public int MajorBugs { get; set; }
        public int MinorBugs { get; set; }
        public int DoneBugs { get; set; }
        public int ReOpenBugs { get; set; }
        public int YearWeek { get; set; }
        public ProjectProxy Project { get; set; }
    }
}