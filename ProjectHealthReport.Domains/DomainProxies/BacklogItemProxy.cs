using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Mappings;

namespace ProjectHealthReport.Domains.DomainProxies
{
    public class BacklogItemProxy : IWeeklyReport, IMapFrom<BacklogItem>, IMapTo<BacklogItem>
    {
        public BacklogItemProxy()
        {
            
        }
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int? Sprint { get; set; }
        public int ItemsAdded { get; set; }
        public int? StoryPointsAdded { get; set; }
        public int ItemsDone { get; set; }
        public int? StoryPointsDone { get; set; }
        public int YearWeek { get; set; }
        public ProjectProxy Project { get; set; }
    }
}