namespace ProjectHealthReport.Domains.Domains
{
    public class BacklogItem : IWeeklyReport
    {
        public BacklogItem()
        {
            
        }
        public BacklogItem(int id, int projectId, int? sprint, int itemsAdded, int? storyPointsAdded, int itemsDone,
            int? storyPointsDone, int yearWeek, Project project)
        {
            Id = id;
            ProjectId = projectId;
            Sprint = sprint;
            ItemsAdded = itemsAdded;
            StoryPointsAdded = storyPointsAdded;
            ItemsDone = itemsDone;
            StoryPointsDone = storyPointsDone;
            YearWeek = yearWeek;
            Project = project;
        }

        public int Id { get; private set; }
        public int ProjectId { get; private set; }
        public int? Sprint { get; private set; }
        public int ItemsAdded { get; private set; }
        public int? StoryPointsAdded { get; private set; }
        public int ItemsDone { get; private set; }
        public int? StoryPointsDone { get; private set; }
        public int YearWeek { get; set; }
        public Project Project { get; private set; }
    }
}