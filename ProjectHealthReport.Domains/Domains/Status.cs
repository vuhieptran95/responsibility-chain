using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectHealthReport.Domains.Domains
{
    public class Status : IWeeklyReport
    {
        public Status()
        {
        }

        public Status(int id, int projectId, string statusColor, string projectStatus, string retrospectiveFeedBack,
            DateTime? milestoneDate, string milestone, int yearWeek, Project project)
        {
            Id = id;
            ProjectId = projectId;
            StatusColor = statusColor;
            ProjectStatus = projectStatus;
            RetrospectiveFeedBack = retrospectiveFeedBack;
            MilestoneDate = milestoneDate;
            Milestone = milestone;
            YearWeek = yearWeek;
            Project = project;
        }

        public int Id { get; private set; }
        public int ProjectId { get; private set; }
        [Required] public string StatusColor { get; private set; }
        public string ProjectStatus { get; private set; }
        public string RetrospectiveFeedBack { get; private set; }
        public DateTime? MilestoneDate { get; private set; }
        public string Milestone { get; private set; }
        public int YearWeek { get; set; }
        public Project Project { get; private set; }
    }
}