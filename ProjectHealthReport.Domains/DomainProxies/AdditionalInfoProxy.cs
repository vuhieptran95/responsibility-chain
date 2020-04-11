using System.Collections.Generic;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Mappings;

namespace ProjectHealthReport.Domains.DomainProxies
{
    public class AdditionalInfoProxy : IWeeklyReport, IMapFrom<AdditionalInfo>, IMapTo<AdditionalInfo>
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int YearWeek { get; set; }
        public ProjectProxy Project { get; set; }
        public ICollection<AdditionalInfoIssuesProxy> AdditionalInfoIssues { get; set; }
    }
}