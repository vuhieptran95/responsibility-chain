namespace ProjectHealthReport.Domains.Domains
{
    public class WeeklyReportStatus
    {
        private readonly int _id;
        private readonly int _projectId;
        private readonly int _yearWeek;
        private readonly bool? _isDeadlineMissed;
        private readonly Project _project;

        public WeeklyReportStatus()
        {
            
        }

        public WeeklyReportStatus(int id, int projectId, int yearWeek, bool? isDeadlineMissed, Project project) : this()
        {
            _id = id;
            _projectId = projectId;
            _yearWeek = yearWeek;
            _isDeadlineMissed = isDeadlineMissed;
            _project = project;
        }

        public WeeklyReportStatus(int id, int projectId, int yearWeek, bool? isDeadlineMissed) : this()
        {
            _id = id;
            _projectId = projectId;
            _yearWeek = yearWeek;
            _isDeadlineMissed = isDeadlineMissed;
        }

        public int Id => _id;

        public int ProjectId => _projectId;

        public int YearWeek => _yearWeek;

        public bool? IsDeadlineMissed => _isDeadlineMissed;

        public Project Project => _project;
    }
}