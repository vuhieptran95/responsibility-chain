namespace ProjectHealthReport.Domains.Domains
{
    public class QualityReport : IWeeklyReport
    {
        public QualityReport()
        {
        }

        public QualityReport(int id, int projectId, int criticalBugs, int majorBugs, int minorBugs, int doneBugs,
            int reOpenBugs, int yearWeek, Project project)
        {
            Id = id;
            ProjectId = projectId;
            CriticalBugs = criticalBugs;
            MajorBugs = majorBugs;
            MinorBugs = minorBugs;
            DoneBugs = doneBugs;
            ReOpenBugs = reOpenBugs;
            YearWeek = yearWeek;
            Project = project;
        }

        public int Id { get; private set; }
        public int ProjectId { get; private set; }
        public int CriticalBugs { get; private set; }
        public int MajorBugs { get; private set; }
        public int MinorBugs { get; private set; }
        public int DoneBugs { get; private set; }
        public int ReOpenBugs { get; private set; }
        public int YearWeek { get; set; }
        public Project Project { get; private set; }
    }
}