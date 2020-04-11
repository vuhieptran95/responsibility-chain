using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using ProjectHealthReport.Domains.Exceptions;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Domains.Migrations;

namespace ProjectHealthReport.Domains.Domains
{
    public partial class Project
    {
        private int _id;
        private string _name;
        private string _code;
        private string _division;
        private string _keyAccountManager;
        private bool _phrRequired;
        private bool _dmrRequired;
        private bool _dodRequired;
        private int _projectStateTypeId;
        private string _deliveryResponsibleName;
        private string _platformVersion;
        private string _jiraLink;
        private string _sourceCodeLink;
        private string _note;
        private DateTime _createdDate;
        private DateTime _projectStartDate;
        private DateTime? _projectEndDate;
        private DateTime? _phrRequiredFrom;
        private DateTime? _dmrRequiredFrom;
        private DateTime? _dmrRequiredTo;
        private ProjectStateType _projectStateType;
        private ICollection<Status> _statuses;
        private ICollection<BacklogItem> _backlogItems;
        private ICollection<QualityReport> _qualityReports;
        private ICollection<AdditionalInfo> _additionalInfos;
        private ICollection<DivisionProjectStatus> _divisionProjectStatuses;
        private ICollection<WeeklyReportStatus> _weeklyReportStatuses;
        private ICollection<ProjectAccess> _projectAccesses;
        private ICollection<DoDReport> _doDReports;

        public Project()
        {
            _statuses = new HashSet<Status>();
            _backlogItems = new HashSet<BacklogItem>();
            _qualityReports = new HashSet<QualityReport>();
            _additionalInfos = new HashSet<AdditionalInfo>();
            _divisionProjectStatuses = new HashSet<DivisionProjectStatus>();
            _weeklyReportStatuses = new HashSet<WeeklyReportStatus>();
            _projectAccesses = new HashSet<ProjectAccess>();
            _doDReports = new HashSet<DoDReport>();
        }

        public Project(int id, string name, string code, string division, string keyAccountManager,
            DateTime projectStartDate,
            bool phrRequired, bool dmrRequired, bool dodRequired, int projectStateTypeId,
            List<(string, string)> userRoleList, DateTime createdDate, string deliveryResponsibleName, string platformVersion, string jiraLink,
            string sourceCodeLink, string note,
            DateTime? projectEndDate,
            DateTime? phrRequiredFrom, DateTime? dmrRequiredFrom,
            DateTime? dmrRequiredTo, ProjectStateType projectStateType,
            List<Status> statuses, List<BacklogItem> backlogItems,
            List<QualityReport> qualityReports, List<AdditionalInfo> additionalInfos,
            List<DivisionProjectStatus> divisionProjectStatuses,
            List<WeeklyReportStatus> weeklyReportStatuses, List<ProjectAccess> projectAccesses,
            List<DoDReport> doDReports) : this()
        {
            _id = id;
            _name = name;
            _code = code;
            _division = division;
            _keyAccountManager = keyAccountManager;
            _deliveryResponsibleName = deliveryResponsibleName;
            _platformVersion = platformVersion;
            _jiraLink = jiraLink;
            _sourceCodeLink = sourceCodeLink;
            _note = note;
            _projectStartDate = projectStartDate;
            _projectEndDate = projectEndDate;
            _phrRequired = phrRequired;
            _phrRequiredFrom = phrRequiredFrom;
            _dmrRequired = dmrRequired;
            _dodRequired = dodRequired;
            _createdDate = createdDate;
            _dmrRequiredFrom = dmrRequiredFrom;
            _dmrRequiredTo = dmrRequiredTo;
            _projectStateTypeId = projectStateTypeId;
            _projectStateType = projectStateType;
            _statuses = statuses ?? Statuses;
            _backlogItems = backlogItems ?? BacklogItems;
            _qualityReports = qualityReports ?? QualityReports;
            _additionalInfos = additionalInfos ?? AdditionalInfos;
            _divisionProjectStatuses = divisionProjectStatuses ?? DivisionProjectStatuses;
            _weeklyReportStatuses = weeklyReportStatuses ?? WeeklyReportStatuses;
            _projectAccesses = projectAccesses ?? ProjectAccesses;
            _doDReports = doDReports ?? DoDReports;

            ValidateDomain(userRoleList);
        }

        public int Id => _id;

        [Required]
        public string Name => _name;

        [Required]
        public string Code => _code;

        [Required]
        public string Division => _division;

        [Required]
        public string KeyAccountManager => _keyAccountManager;

        public bool PhrRequired => _phrRequired;

        public bool DmrRequired => _dmrRequired;

        public bool DodRequired => _dodRequired;

        public int ProjectStateTypeId => _projectStateTypeId;

        public string DeliveryResponsibleName => _deliveryResponsibleName;

        public string PlatformVersion => _platformVersion;

        public string JiraLink => _jiraLink;

        public string SourceCodeLink => _sourceCodeLink;

        public string Note => _note;

        public DateTime CreatedDate => _createdDate;

        public DateTime ProjectStartDate => _projectStartDate;

        public DateTime? ProjectEndDate => _projectEndDate;

        public DateTime? PhrRequiredFrom => _phrRequiredFrom;

        public DateTime? DmrRequiredFrom => _dmrRequiredFrom;

        public DateTime? DmrRequiredTo => _dmrRequiredTo;

        public ProjectStateType ProjectStateType => _projectStateType;

        public ICollection<Status> Statuses => _statuses;

        public ICollection<BacklogItem> BacklogItems => _backlogItems;

        public ICollection<QualityReport> QualityReports => _qualityReports;

        public ICollection<AdditionalInfo> AdditionalInfos => _additionalInfos;

        public ICollection<DivisionProjectStatus> DivisionProjectStatuses => _divisionProjectStatuses;

        public ICollection<WeeklyReportStatus> WeeklyReportStatuses => _weeklyReportStatuses;

        public ICollection<ProjectAccess> ProjectAccesses => _projectAccesses;

        public ICollection<DoDReport> DoDReports => _doDReports;
    }

    public partial class Project
    {
        public void ValidateDomain(List<(string, string)> userRoleList)
        {
            OrganizationInfoMustBeValid(userRoleList);
            ValidateLinkProperties();
            ValidateProjectEndDate();
            ValidatePhrRequired();
            ValidateDmrRequired();
            ValidateProjectCode();
        }

        public void ValidateProjectCode()
        {
            if (Regex.IsMatch(Code, "^[a-zA-Z0-9]+$") && Code.Length < 4 && Code.ToUpper() == Code)
            {
                return;
            }
            DomainExceptionCode.Throw(DomainError.D001, this);
        }

        public void OrganizationInfoMustBeValid(List<(string, string)> userRoleList)
        {
            if (!AuthorizationHelper.DeliveryManagers.Select(i => i.Value).Contains(Division))
            {
                DomainExceptionCode.Throw(DomainError.D002, this);
            }

            if (!userRoleList.Where(i => i.Item2 == AuthorizationHelper.RoleKam).Select(i => i.Item1)
                .Contains(KeyAccountManager))
            {
                DomainExceptionCode.Throw(DomainError.D003, this);
            }

            if (DeliveryResponsibleName != null && !userRoleList.Where(i => i.Item2 == AuthorizationHelper.RolePic).Select(i => i.Item1)
                .Contains(DeliveryResponsibleName))
            {
                DomainExceptionCode.Throw(DomainError.D004, this);
            }
        }

        public void ValidateLinkProperties()
        {
            if (JiraLink != null && !MiscHelper.ValidateLink(JiraLink))
            {
                DomainExceptionCode.Throw(DomainError.D005, this);
            }

            if (SourceCodeLink != null && !MiscHelper.ValidateLink(SourceCodeLink))
            {
                DomainExceptionCode.Throw(DomainError.D006, this);
            }

            
        }

        public void ValidateProjectEndDate()
        {
            if (ProjectEndDate.HasValue && ProjectEndDate.Value > ProjectStartDate)
            {
                DomainExceptionCode.Throw(DomainError.D007, this);
            }
        }

        public void ValidatePhrRequired()
        {
            if (PhrRequired && !PhrRequiredFrom.HasValue)
            {
                DomainExceptionCode.Throw(DomainError.D008, this);
            }

            if (PhrRequired && DeliveryResponsibleName == null)
            {
                DomainExceptionCode.Throw(DomainError.D013, this);
            }

            if (DodRequired && !PhrRequired)
            {
                DomainExceptionCode.Throw(DomainError.D011, this);
            }
        }

        public void ValidateDmrRequired()
        {
            if (DmrRequired && !DmrRequiredFrom.HasValue)
            {
                DomainExceptionCode.Throw(DomainError.D009, this);
            }

            if (DmrRequired && DmrRequiredFrom.HasValue && DmrRequiredTo.HasValue &&
                DmrRequiredFrom.Value > DmrRequiredTo.Value)
            {
                DomainExceptionCode.Throw(DomainError.D010, this);
            }
        }
    }
}