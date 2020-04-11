using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Mappings;

namespace ProjectHealthReport.Domains.DomainProxies
{
    public class DivisionProjectStatusProxy : IWeeklyReport, IMapFrom<DivisionProjectStatus>, IMapTo<DivisionProjectStatus>
    {
        public int Id { get; set; }
        public int YearWeek { get; set; }
        public int ProjectId { get; set; }
        public string StatusColor { get; set; }
        public string ProjectStatus { get; set; }
        public string Actions { get; set; }
        public ProjectProxy Project { get; set; }
    }
}