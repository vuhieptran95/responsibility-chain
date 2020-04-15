using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectHealthReport.Domains.Domains
{
    public class Status : IWeeklyReport
    {
        private int _id;
        private Project _project;
        private int _yearWeek;
        private string _milestone;
        private DateTime? _milestoneDate;
        private string _retrospectiveFeedBack;
        private string _projectStatus;
        private string _statusColor;
        private int _projectId;

        public Status()
        {
        }

        public Status(int id, int projectId, string statusColor, string projectStatus, string retrospectiveFeedBack,
            DateTime? milestoneDate, string milestone, int yearWeek, Project project) : this(id, projectId, statusColor,
            projectStatus, retrospectiveFeedBack, milestoneDate, milestone, yearWeek)
        {
            _project = project;
        }

        public Status(int id, int projectId, string statusColor,
            string projectStatus, string retrospectiveFeedBack,
            DateTime? milestoneDate, string milestone, int yearWeek)
        {
            _id = id;
            _yearWeek = yearWeek;
            _milestone = milestone;
            _milestoneDate = milestoneDate;
            _retrospectiveFeedBack = retrospectiveFeedBack;
            _projectStatus = projectStatus;
            _statusColor = statusColor;
            _projectId = projectId;
        }

        public void UpdateValue(int id, int yearWeek, string milestone, DateTime? milestoneDate,
            string retrospectiveFeedBack,
            string projectStatus, string statusColor, int projectId)
        {
            _id = id;
            _yearWeek = yearWeek;
            _milestone = milestone;
            _milestoneDate = milestoneDate;
            _retrospectiveFeedBack = retrospectiveFeedBack;
            _projectStatus = projectStatus;
            _statusColor = statusColor;
            _projectId = projectId;
        }

        public void UpdateValue(Status s)
        {
            UpdateValue(Id, s.YearWeek, s.Milestone, s.MilestoneDate, s.RetrospectiveFeedBack, s.ProjectStatus,
                s.StatusColor, s.ProjectId);
        }

        public int Id => _id;

        public int ProjectId => _projectId;

        public string StatusColor => _statusColor;

        public string ProjectStatus => _projectStatus;

        public string RetrospectiveFeedBack => _retrospectiveFeedBack;

        public DateTime? MilestoneDate => _milestoneDate;

        public string Milestone => _milestone;

        public int YearWeek
        {
            get => _yearWeek;
            set => _yearWeek = value;
        }

        public Project Project => _project;
    }
}