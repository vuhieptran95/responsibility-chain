namespace ProjectHealthReport.Domains.Domains
{
    public class QualityReport : IWeeklyReport
    {
        private Project _project;
        private int _yearWeek;
        private int _reOpenBugs;
        private int _doneBugs;
        private int _minorBugs;
        private int _majorBugs;
        private int _criticalBugs;
        private int _projectId;
        private int _id;

        public QualityReport()
        {
        }

        public QualityReport(int id, int projectId, int criticalBugs, int majorBugs, int minorBugs, int doneBugs,
            int reOpenBugs, int yearWeek, Project project)
        {
            _id = id;
            _projectId = projectId;
            _criticalBugs = criticalBugs;
            _majorBugs = majorBugs;
            _minorBugs = minorBugs;
            _doneBugs = doneBugs;
            _reOpenBugs = reOpenBugs;
            YearWeek = yearWeek;
            _project = project;
        }

        public QualityReport(int yearWeek, int reOpenBugs, int doneBugs, int minorBugs, int majorBugs, int criticalBugs, int projectId, int id)
        {
            _yearWeek = yearWeek;
            _reOpenBugs = reOpenBugs;
            _doneBugs = doneBugs;
            _minorBugs = minorBugs;
            _majorBugs = majorBugs;
            _criticalBugs = criticalBugs;
            _projectId = projectId;
            _id = id;
        }

        public int Id => _id;

        public int ProjectId => _projectId;

        public int CriticalBugs => _criticalBugs;

        public int MajorBugs => _majorBugs;

        public int MinorBugs => _minorBugs;

        public int DoneBugs => _doneBugs;

        public int ReOpenBugs => _reOpenBugs;

        public int YearWeek
        {
            get => _yearWeek;
            set => _yearWeek = value;
        }

        public Project Project => _project;
    }
}