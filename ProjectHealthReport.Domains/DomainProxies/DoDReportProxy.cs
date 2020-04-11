using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Mappings;

namespace ProjectHealthReport.Domains.DomainProxies
{
    public class DoDReportProxy : IMapFrom<DoDReport>, IMapTo<DoDReport>
    {
        public int ProjectId { get; set; }
        public int MetricId { get; set; }
        public int YearWeek { get; set; }
        public string Value { get; set; }
        public ProjectProxy Project { get; set; }
        public MetricProxy Metric { get; set; }
        public string LinkToReport { get; set; }
        public string ReportFileName { get; set; }
    }
}