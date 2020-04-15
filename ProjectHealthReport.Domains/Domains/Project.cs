using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        private ICollection<DivisionProjectStatus> _divisionProjectStatuses;
        private ICollection<WeeklyReportStatus> _weeklyReportStatuses;
        private ICollection<ProjectAccess> _projectAccesses;
        private ICollection<DoDReport> _doDReports;

        public Project()
        {
            _statuses = new HashSet<Status>();
            _backlogItems = new HashSet<BacklogItem>();
            _qualityReports = new HashSet<QualityReport>();
            _divisionProjectStatuses = new HashSet<DivisionProjectStatus>();
            _weeklyReportStatuses = new HashSet<WeeklyReportStatus>();
            _projectAccesses = new HashSet<ProjectAccess>();
            _doDReports = new HashSet<DoDReport>();
        }

        public Project(int id, string name, string code, string division, string keyAccountManager,
            DateTime projectStartDate,
            bool phrRequired, bool dmrRequired, bool dodRequired, int projectStateTypeId,
            List<(string, string)> userRoleList, DateTime createdDate, string deliveryResponsibleName,
            string platformVersion, string jiraLink,
            string sourceCodeLink, string note,
            DateTime? projectEndDate,
            DateTime? phrRequiredFrom, DateTime? dmrRequiredFrom,
            DateTime? dmrRequiredTo, ProjectStateType projectStateType,
            List<Status> statuses, List<BacklogItem> backlogItems,
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
            _statuses = statuses;
            _backlogItems = backlogItems;
            _divisionProjectStatuses = divisionProjectStatuses;
            _weeklyReportStatuses = weeklyReportStatuses;
            _projectAccesses = projectAccesses;
            _doDReports = doDReports;

            ValidateDomain(userRoleList);
        }

        public Project(int id, string name, string code, string division, string keyAccountManager, bool phrRequired,
            bool dmrRequired, bool dodRequired, int projectStateTypeId, string deliveryResponsibleName,
            string platformVersion, string jiraLink, string sourceCodeLink, string note, DateTime createdDate,
            DateTime projectStartDate, DateTime? projectEndDate, DateTime? phrRequiredFrom, DateTime? dmrRequiredFrom,
            DateTime? dmrRequiredTo) : this()
        {
            _id = id;
            _name = name;
            _code = code;
            _division = division;
            _keyAccountManager = keyAccountManager;
            _phrRequired = phrRequired;
            _dmrRequired = dmrRequired;
            _dodRequired = dodRequired;
            _projectStateTypeId = projectStateTypeId;
            _deliveryResponsibleName = deliveryResponsibleName;
            _platformVersion = platformVersion;
            _jiraLink = jiraLink;
            _sourceCodeLink = sourceCodeLink;
            _note = note;
            _createdDate = createdDate;
            _projectStartDate = projectStartDate;
            _projectEndDate = projectEndDate;
            _phrRequiredFrom = phrRequiredFrom;
            _dmrRequiredFrom = dmrRequiredFrom;
            _dmrRequiredTo = dmrRequiredTo;
        }

        public int Id => _id;

        public string Name => _name;

        public string Code => _code;

        public string Division => _division;

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

        public IEnumerable<Status> Statuses => _statuses;

        public IEnumerable<BacklogItem> BacklogItems => _backlogItems;

        public IEnumerable<QualityReport> QualityReports => _qualityReports;

        public IEnumerable<DivisionProjectStatus> DivisionProjectStatuses => _divisionProjectStatuses;

        public IEnumerable<WeeklyReportStatus> WeeklyReportStatuses => _weeklyReportStatuses;

        public IEnumerable<ProjectAccess> ProjectAccesses => _projectAccesses;

        public IEnumerable<DoDReport> DoDReports => _doDReports;
    }
}