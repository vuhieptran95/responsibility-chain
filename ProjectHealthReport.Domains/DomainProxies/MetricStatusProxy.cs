using System.Collections.Generic;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Mappings;

namespace ProjectHealthReport.Domains.DomainProxies
{
    public class MetricStatusProxy : IMapFrom<MetricStatus>, IMapTo<MetricStatus>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ThresholdProxy> Thresholds { get; set; }
    }
}