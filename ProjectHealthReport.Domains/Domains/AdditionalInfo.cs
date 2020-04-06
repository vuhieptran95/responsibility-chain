using System.Collections.Generic;

namespace ProjectHealthReport.Domains.Domains
{
    public class AdditionalInfo : IWeeklyReport
    {
        public AdditionalInfo()
        {
            AdditionalInfoIssues = new HashSet<AdditionalInfoIssues>();
        }

        public AdditionalInfo(int id, int projectId, int yearWeek, Project project,
            ICollection<AdditionalInfoIssues> additionalInfoIssues) : this()
        {
            Id = id;
            ProjectId = projectId;
            YearWeek = yearWeek;
            Project = project;
            AdditionalInfoIssues = additionalInfoIssues ?? AdditionalInfoIssues;
        }

        public int Id { get; private set; }
        public int ProjectId { get; private set; }
        public int YearWeek { get; set; }
        public Project Project { get; private set; }
        public ICollection<AdditionalInfoIssues> AdditionalInfoIssues { get; private set; }
    }
}