using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Mappings;

namespace ProjectHealthReport.Domains.DomainProxies
{
    public class ThresholdProxy : IMapFrom<Threshold>, IMapTo<Threshold>
    {
        public int MetricStatusId { get; set; }
        public int MetricId { get; set; }
        public decimal? UpperBound { get; set; }
        public decimal? LowerBound { get; set; }
        public string UpperBoundOperator { get; set; }
        public string LowerBoundOperator { get; set; }
        public bool IsRange { get; set; }
        public string Value { get; set; }
        public MetricProxy Metric { get; set; }
        public MetricStatusProxy MetricStatus { get; set; }
    }
}