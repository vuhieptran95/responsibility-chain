using System;
using MessagePack;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Mappings;

namespace ProjectHealthReport.Domains.DomainProxies
{
    [MessagePackObject]
    public class StatusProxy : IWeeklyReport, IMapFrom<Status>, IMapTo<Status>
    {
        [Key(0)]
        public int Id { get; set; }
        [Key(1)]
        public int ProjectId { get; set; }
        [Key(2)]
        public string StatusColor { get; set; }
        [Key(3)]
        public string ProjectStatus { get; set; }
        [Key(4)]
        public string RetrospectiveFeedBack { get; set; }
        [Key(5)]
        public DateTime? MilestoneDate { get; set; }
        [Key(6)]
        public string Milestone { get; set; }
        [Key(7)]
        public int YearWeek { get; set; }
        [IgnoreMember]
        public ProjectProxy Project { get; set; }
    }
}