using MessagePack;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Mappings;

namespace ProjectHealthReport.Domains.DomainProxies
{
    [MessagePackObject]
    public class QualityReportProxy : IWeeklyReport, IMapFrom<QualityReport>, IMapTo<QualityReport>
    {
        [Key(0)]
        public int Id { get; set; }
        [Key(1)]
        public int ProjectId { get; set; }
        [Key(2)]
        public int CriticalBugs { get; set; }
        [Key(3)]
        public int MajorBugs { get; set; }
        [Key(4)]
        public int MinorBugs { get; set; }
        [Key(5)]
        public int DoneBugs { get; set; }
        [Key(6)]
        public int ReOpenBugs { get; set; }
        [Key(7)]
        public int YearWeek { get; set; }
        [IgnoreMember]
        public ProjectProxy Project { get; set; }
    }
}