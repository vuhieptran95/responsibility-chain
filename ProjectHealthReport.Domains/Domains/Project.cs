using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectHealthReport.Domains.Domains
{
    public partial class Project
    {
        public Project()
        {
            Statuses = new HashSet<Status>();
            BacklogItems = new HashSet<BacklogItem>();
            QualityReports = new HashSet<QualityReport>();
            AdditionalInfos = new HashSet<AdditionalInfo>();
            DivisionProjectStatuses = new HashSet<DivisionProjectStatus>();
            Milestones = new HashSet<Milestone>();
            WeeklyReportStatuses = new HashSet<WeeklyReportStatus>();
            ProjectAccesses = new HashSet<ProjectAccess>();
            DoDReports = new HashSet<DoDReport>();
        }

        public int Id { get; set; }

        [Required] public string Name { get; set; }

        [Required] public string Code { get; set; }

        [Required] public string Division { get; set; }

        [Required] public string KeyAccountManager { get; set; }

        public string DeliveryResponsibleName { get; set; }

        public string PlatformVersion { get; set; }

        public string JIRALink { get; set; }

        public string SourceCodeLink { get; set; }

        public string Note { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ProjectStartDate { get; set; }

        public DateTime? ProjectEndDate { get; set; }

        public DateTime? SprintStartDate { get; set; }

        public DateTime? SprintEndDate { get; set; }

        public DateTime? NextMileStoneDate { get; set; }
        public bool PhrRequired { get; set; }
        public DateTime? PhrRequiredFrom { get; set; }
        public bool DmrRequired { get; set; }
        public bool DodRequired { get; set; }
        public DateTime? DmrRequiredFrom { get; set; }
        public DateTime? DmrRequiredTo { get; set; }
        public int ProjectStateTypeId { get; set; }
        public ProjectStateType ProjectStateType { get; set; }
        public ICollection<Status> Statuses { get; set; }
        public ICollection<BacklogItem> BacklogItems { get; set; }
        public ICollection<QualityReport> QualityReports { get; set; }
        public ICollection<AdditionalInfo> AdditionalInfos { get; set; }
        public ICollection<DivisionProjectStatus> DivisionProjectStatuses { get; set; }
        public ICollection<Milestone> Milestones { get; set; }
        public ICollection<WeeklyReportStatus> WeeklyReportStatuses { get; set; }
        public ICollection<ProjectAccess> ProjectAccesses { get; set; }
        public ICollection<DoDReport> DoDReports { get; set; }
    }
}