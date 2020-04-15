using MessagePack;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Mappings;

namespace ProjectHealthReport.Domains.DomainProxies
{
    [MessagePackObject]
    public class BacklogItemProxy : IWeeklyReport, IMapFrom<BacklogItem>, IMapTo<BacklogItem>
    {
        public BacklogItemProxy()
        {
            
        }
        [Key(0)]
        public int Id { get; set; }
        [Key(1)]
        public int ProjectId { get; set; }
        [Key(2)]
        public int? Sprint { get; set; }
        [Key(3)]
        public int ItemsAdded { get; set; }
        [Key(4)]
        public int? StoryPointsAdded { get; set; }
        [Key(5)]
        public int ItemsDone { get; set; }
        [Key(6)]
        public int? StoryPointsDone { get; set; }
        [Key(7)]
        public int YearWeek { get; set; }
        [IgnoreMember]
        public ProjectProxy Project { get; set; }
    }
}
