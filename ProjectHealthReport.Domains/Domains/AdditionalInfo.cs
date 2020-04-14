using System.Collections.Generic;

namespace ProjectHealthReport.Domains.Domains
{
    public class AdditionalInfo : IWeeklyReport
    {
        private ICollection<AdditionalInfoIssues> _additionalInfoIssues;
        private Project _project;
        private int _yearWeek;
        private int _projectId;
        private int _id;

        public AdditionalInfo()
        {
            _additionalInfoIssues = new HashSet<AdditionalInfoIssues>();
        }

        public AdditionalInfo(int id, int projectId, int yearWeek, Project project,
            ICollection<AdditionalInfoIssues> additionalInfoIssues) : this()
        {
            _id = id;
            _projectId = projectId;
            YearWeek = yearWeek;
            _project = project;
            _additionalInfoIssues = additionalInfoIssues;
        }

        public AdditionalInfo(int yearWeek, int projectId, int id) : this()
        {
            _yearWeek = yearWeek;
            _projectId = projectId;
            _id = id;
        }

        public int Id => _id;

        public int ProjectId => _projectId;

        public int YearWeek
        {
            get => _yearWeek;
            set => _yearWeek = value;
        }

        public Project Project => _project;

        public IEnumerable<AdditionalInfoIssues> AdditionalInfoIssues => _additionalInfoIssues;
    }
}