using System.Collections.Generic;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Mappings;

namespace ProjectHealthReport.Domains.DomainProxies
{
    public class IssueProxy: IMapFrom<Issue>, IMapTo<Issue>
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public string Impact { get; set; }
        public string Action { get; set; }
        public int OpenedYearWeek { get; set; }
        public ICollection<AdditionalInfoIssuesProxy> AdditionalInfoIssues { get; set; }
    }
}