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
            int reOpenBugs, int yearWeek, Project project) : this(id, projectId, criticalBugs, majorBugs, minorBugs,
            doneBugs, reOpenBugs, yearWeek)
        {
            _project = project;
        }

        public QualityReport(int id, int projectId, int criticalBugs, int majorBugs, int minorBugs, int doneBugs,
            int reOpenBugs, int yearWeek)
        {
            _id = id;
            _projectId = projectId;
            _criticalBugs = criticalBugs;
            _majorBugs = majorBugs;
            _minorBugs = minorBugs;
            _doneBugs = doneBugs;
            _reOpenBugs = reOpenBugs;
            YearWeek = yearWeek;
        }

        public void UpdateValue(int id, int projectId, int criticalBugs, int majorBugs, int minorBugs, int doneBugs,
            int reOpenBugs, int yearWeek)
        {
            _id = id;
            _projectId = projectId;
            _criticalBugs = criticalBugs;
            _majorBugs = majorBugs;
            _minorBugs = minorBugs;
            _doneBugs = doneBugs;
            _reOpenBugs = reOpenBugs;
            YearWeek = yearWeek;
        }

        public void UpdateValue(QualityReport report)
        {
            UpdateValue(report.Id, report.ProjectId, report.CriticalBugs, report.MajorBugs, report.MinorBugs,
                report.DoneBugs, report.ReOpenBugs, report.YearWeek);
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