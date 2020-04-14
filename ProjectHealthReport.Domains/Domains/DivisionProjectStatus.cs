using System.ComponentModel.DataAnnotations;

namespace ProjectHealthReport.Domains.Domains
{
    public class DivisionProjectStatus : IWeeklyReport
    {
        private int _id;
        private int _yearWeek;
        private Project _project;
        private string _actions;
        private string _projectStatus;
        private string _statusColor;
        private int _projectId;

        public DivisionProjectStatus()
        {
            
        }

        public DivisionProjectStatus(int id, int yearWeek, int projectId, string statusColor, string projectStatus, string actions, Project project)
        {
            _id = id;
            YearWeek = yearWeek;
            _projectId = projectId;
            _statusColor = statusColor;
            _projectStatus = projectStatus;
            _actions = actions;
            _project = project;
        }

        public DivisionProjectStatus(int id, int yearWeek, int projectId, string statusColor, string projectStatus, string actions)
        {
            _id = id;
            YearWeek = yearWeek;
            _projectId = projectId;
            _statusColor = statusColor;
            _projectStatus = projectStatus;
            _actions = actions;
        }

        public int Id => _id;

        public int YearWeek
        {
            get => _yearWeek;
            set => _yearWeek = value;
        }

        public int ProjectId => _projectId;

        public string StatusColor => _statusColor;

        public string ProjectStatus => _projectStatus;

        public string Actions => _actions;

        public Project Project => _project;
    }
}
