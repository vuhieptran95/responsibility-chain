namespace ProjectHealthReport.Domains.Domains
{
    public class BacklogItem : IWeeklyReport
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int? Sprint { get; set; }
        public int ItemsAdded { get; set; }
        public int? StoryPointsAdded { get; set; }
        public int ItemsDone { get; set; }
        public int? StoryPointsDone { get; set; }
        public int YearWeek { get; set; }
        public Project Project { get; set; }
    }
}