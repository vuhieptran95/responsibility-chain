using System.ComponentModel.DataAnnotations;

namespace ProjectHealthReport.Domains.Domains
{
    public class BacklogItem : IWeeklyReport
    {
        [Key] private int _id;
        private int _projectId;
        private int? _sprint;
        private int _itemsAdded;
        private int? _storyPointsAdded;
        private int _itemsDone;
        private int? _storyPointsDone;
        private int _yearWeek;
        private Project _project;

        public BacklogItem()
        {
        }

        public BacklogItem(int id, int projectId, int? sprint, int itemsAdded, int? storyPointsAdded, int itemsDone,
            int? storyPointsDone, int yearWeek, Project project) : this(id, projectId, sprint, itemsAdded,
            storyPointsAdded, itemsDone, storyPointsDone, yearWeek)
        {
            _project = project;
        }

        public BacklogItem(int id, int projectId, int? sprint, int itemsAdded, int? storyPointsAdded, int itemsDone,
            int? storyPointsDone, int yearWeek)
        {
            _id = id;
            _projectId = projectId;
            _sprint = sprint;
            _itemsAdded = itemsAdded;
            _storyPointsAdded = storyPointsAdded;
            _itemsDone = itemsDone;
            _storyPointsDone = storyPointsDone;
            YearWeek = yearWeek;
        }

        public void UpdateValue(int id, int projectId, int? sprint, int itemsAdded, int? storyPointsAdded,
            int itemsDone,
            int? storyPointsDone, int yearWeek)
        {
            _id = id;
            _projectId = projectId;
            _sprint = sprint;
            _itemsAdded = itemsAdded;
            _storyPointsAdded = storyPointsAdded;
            _itemsDone = itemsDone;
            _storyPointsDone = storyPointsDone;
            YearWeek = yearWeek;
        }

        public void UpdateValue(BacklogItem item)
        {
            UpdateValue(item.Id, item.ProjectId, item.Sprint, item.ItemsAdded, item.StoryPointsAdded, item.ItemsDone,
                item.StoryPointsDone, item.YearWeek);
        }

        public int Id => _id;

        public int ProjectId => _projectId;

        public int? Sprint => _sprint;

        public int ItemsAdded => _itemsAdded;

        public int? StoryPointsAdded => _storyPointsAdded;

        public int ItemsDone => _itemsDone;

        public int? StoryPointsDone => _storyPointsDone;

        public int YearWeek
        {
            get => _yearWeek;
            set => _yearWeek = value;
        }

        public Project Project => _project;
    }
}