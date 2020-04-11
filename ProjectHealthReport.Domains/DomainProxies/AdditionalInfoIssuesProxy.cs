using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Mappings;

namespace ProjectHealthReport.Domains.DomainProxies
{
    public class AdditionalInfoIssuesProxy: IMapFrom<AdditionalInfoIssues>, IMapTo<AdditionalInfoIssues>
    {
        public int AdditionalInfoId { get; set; }
        public int IssueId { get; set; }
        public string Status { get; set; }
        public AdditionalInfoProxy AdditionalInfo { get; set; }
        public IssueProxy Issue { get; set; }
    }
}