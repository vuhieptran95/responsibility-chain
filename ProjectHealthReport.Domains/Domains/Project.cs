using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ProjectHealthReport.Domains.Exceptions;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Domains.Migrations;

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
            WeeklyReportStatuses = new HashSet<WeeklyReportStatus>();
            ProjectAccesses = new HashSet<ProjectAccess>();
            DoDReports = new HashSet<DoDReport>();
        }

        public Project(int id, string name, string code, string division, string keyAccountManager,
            DateTime projectStartDate,
            bool phrRequired, bool dmrRequired, bool dodRequired, int projectStateTypeId,
            List<(string, string)> userRoleList,
            string deliveryResponsibleName = null, string platformVersion = null, string jiraLink = null,
            string sourceCodeLink = null, string note = null,
            DateTime? projectEndDate = null,
            DateTime? phrRequiredFrom = null, DateTime? dmrRequiredFrom = null,
            DateTime? dmrRequiredTo = null, ProjectStateType projectStateType = null,
            List<Status> statuses = null, List<BacklogItem> backlogItems = null,
            List<QualityReport> qualityReports = null, List<AdditionalInfo> additionalInfos = null,
            List<DivisionProjectStatus> divisionProjectStatuses = null,
            List<WeeklyReportStatus> weeklyReportStatuses = null, List<ProjectAccess> projectAccesses = null,
            List<DoDReport> doDReports = null)
        {
            Id = id;
            Name = name;
            Code = code;
            Division = division;
            KeyAccountManager = keyAccountManager;
            DeliveryResponsibleName = deliveryResponsibleName;
            PlatformVersion = platformVersion;
            JIRALink = jiraLink;
            SourceCodeLink = sourceCodeLink;
            Note = note;
            ProjectStartDate = projectStartDate;
            ProjectEndDate = projectEndDate;
            PhrRequired = phrRequired;
            PhrRequiredFrom = phrRequiredFrom;
            DmrRequired = dmrRequired;
            DodRequired = dodRequired;
            DmrRequiredFrom = dmrRequiredFrom;
            DmrRequiredTo = dmrRequiredTo;
            ProjectStateTypeId = projectStateTypeId;
            ProjectStateType = projectStateType;
            Statuses = statuses ?? Statuses;
            BacklogItems = backlogItems ?? BacklogItems;
            QualityReports = qualityReports ?? QualityReports;
            AdditionalInfos = additionalInfos ?? AdditionalInfos;
            DivisionProjectStatuses = divisionProjectStatuses ?? DivisionProjectStatuses;
            WeeklyReportStatuses = weeklyReportStatuses ?? WeeklyReportStatuses;
            ProjectAccesses = projectAccesses ?? ProjectAccesses;
            DoDReports = doDReports ?? DoDReports;

            ValidateDomain(userRoleList);
        }

        public int Id { get; private set; }

        [Required] public string Name { get; private set; }

        [Required] public string Code { get; private set; }

        [Required] public string Division { get; private set; }

        [Required] public string KeyAccountManager { get; private set; }
        public bool PhrRequired { get; private set; }
        public bool DmrRequired { get; private set; }
        public bool DodRequired { get; private set; }
        public int ProjectStateTypeId { get; private set; }

        public string DeliveryResponsibleName { get; private set; }

        public string PlatformVersion { get; private set; }

        public string JIRALink { get; private set; }

        public string SourceCodeLink { get; private set; }

        public string Note { get; private set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ProjectStartDate { get; private set; }
        public DateTime? ProjectEndDate { get; private set; }
        public DateTime? PhrRequiredFrom { get; private set; }
        public DateTime? DmrRequiredFrom { get; private set; }
        public DateTime? DmrRequiredTo { get; private set; }
        public ProjectStateType ProjectStateType { get; private set; }
        public ICollection<Status> Statuses { get; private set; }
        public ICollection<BacklogItem> BacklogItems { get; private set; }
        public ICollection<QualityReport> QualityReports { get; private set; }
        public ICollection<AdditionalInfo> AdditionalInfos { get; private set; }
        public ICollection<DivisionProjectStatus> DivisionProjectStatuses { get; private set; }
        public ICollection<WeeklyReportStatus> WeeklyReportStatuses { get; private set; }
        public ICollection<ProjectAccess> ProjectAccesses { get; private set; }
        public ICollection<DoDReport> DoDReports { get; private set; }
    }

    public partial class Project
    {
        public void SetPhrRequiredFrom(DateTime dateTime)
        {
            if (!PhrRequired)
            {
                DomainExceptionCode.Throw(ErrorCode.D012, this);
            }
            PhrRequiredFrom = dateTime;
            ValidatePhrRequired();
        }
        
        public void ValidateDomain(List<(string, string)> userRoleList)
        {
            OrganizationInfoMustBeValid(userRoleList);
            ValidateLinkProperties();
            ValidateProjectEndDate();
            ValidatePhrRequired();
            ValidateDmrRequired();
        }

        public void OrganizationInfoMustBeValid(List<(string, string)> userRoleList)
        {
            if (!AuthorizationHelper.DeliveryManagers.Select(i => i.Value).Contains(Division))
            {
                DomainExceptionCode.Throw(ErrorCode.D002, this);
            }

            if (!userRoleList.Where(i => i.Item2 == AuthorizationHelper.RoleKam).Select(i => i.Item1)
                .Contains(KeyAccountManager))
            {
                DomainExceptionCode.Throw(ErrorCode.D003, this);
            }

            if (DeliveryResponsibleName != null && !userRoleList.Where(i => i.Item2 == AuthorizationHelper.RolePic).Select(i => i.Item1)
                .Contains(DeliveryResponsibleName))
            {
                DomainExceptionCode.Throw(ErrorCode.D004, this);
            }
        }

        public void ValidateLinkProperties()
        {
            if (JIRALink != null && !ValidateLink(JIRALink))
            {
                DomainExceptionCode.Throw(ErrorCode.D005, this);
            }

            if (SourceCodeLink != null && !ValidateLink(SourceCodeLink))
            {
                DomainExceptionCode.Throw(ErrorCode.D006, this);
            }

            bool ValidateLink(string link)
            {
                Uri uriResult = null;
                return Uri.TryCreate(link, UriKind.Absolute, out uriResult)
                       && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            }
        }

        public void ValidateProjectEndDate()
        {
            if (ProjectEndDate.HasValue && ProjectEndDate.Value > ProjectStartDate)
            {
                DomainExceptionCode.Throw(ErrorCode.D007, this);
            }
        }

        public void ValidatePhrRequired()
        {
            if (PhrRequired && !PhrRequiredFrom.HasValue)
            {
                DomainExceptionCode.Throw(ErrorCode.D008, this);
            }

            if (PhrRequired && DeliveryResponsibleName == null)
            {
                DomainExceptionCode.Throw(ErrorCode.D013, this);
            }

            if (DodRequired && !PhrRequired)
            {
                DomainExceptionCode.Throw(ErrorCode.D011, this);
            }
        }

        public void ValidateDmrRequired()
        {
            if (DmrRequired && !DmrRequiredFrom.HasValue)
            {
                DomainExceptionCode.Throw(ErrorCode.D009, this);
            }

            if (DmrRequired && DmrRequiredFrom.HasValue && DmrRequiredTo.HasValue &&
                DmrRequiredFrom.Value > DmrRequiredTo.Value)
            {
                DomainExceptionCode.Throw(ErrorCode.D010, this);
            }
        }
    }
}