using System.Collections.Generic;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Mappings;

namespace ProjectHealthReport.Domains.DomainProxies
{
    public class MetricProxy : IMapFrom<Metric>, IMapTo<Metric>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ValueType { get; set; }
        public string Unit { get; set; }
        public string Tool { get; set; }
        public string SelectValues { get; set; }
        public int Order { get; set; }
        public int ToolOrder { get; set; }
        public ICollection<ThresholdProxy> Thresholds { get; set; }
        public ICollection<DoDReportProxy> DoDReports { get; set; }
    }
}