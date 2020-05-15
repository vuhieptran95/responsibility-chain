using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ProjectHealthReport.Domains.Migrations;

namespace ProjectHealthReport.Domains.Domains
{
    public partial class Project
    {
        protected int _id;
        protected string _name;
        protected string _code;
        protected string _division;
        protected string _keyAccountManager;
        protected bool _phrRequired;
        protected bool _dmrRequired;
        protected bool _dodRequired;
        protected int _projectStateTypeId;
        protected string _deliveryResponsibleName;
        protected string _platformVersion;
        protected string _jiraLink;
        protected string _sourceCodeLink;
        protected string _note;
        protected DateTime _createdDate;
        protected DateTime _projectStartDate;
        protected DateTime? _projectEndDate;
        protected DateTime? _phrRequiredFrom;
        protected DateTime? _dmrRequiredFrom;
        protected DateTime? _dmrRequiredTo;
        protected ProjectStateType _projectStateType;
        protected ICollection<Status> _statuses;
        protected ICollection<BacklogItem> _backlogItems;
        protected ICollection<QualityReport> _qualityReports;
        protected ICollection<DivisionProjectStatus> _divisionProjectStatuses;
        protected ICollection<WeeklyReportStatus> _weeklyReportStatuses;
        protected ICollection<ProjectAccess> _projectAccesses;
        protected ICollection<DoDReport> _doDReports;

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
            List<QualityReport> qualityReports,
            List<DivisionProjectStatus> divisionProjectStatuses,
            List<WeeklyReportStatus> weeklyReportStatuses, List<ProjectAccess> projectAccesses,
            List<DoDReport> doDReports) : this(id, name, code, division, keyAccountManager, projectStartDate, phrRequired, dmrRequired, dodRequired, projectStateTypeId, userRoleList, createdDate, deliveryResponsibleName, platformVersion, jiraLink, sourceCodeLink, note, projectEndDate, phrRequiredFrom, dmrRequiredFrom, dmrRequiredTo)
        {
            _projectStateType = projectStateType;
            _statuses = statuses;
            _backlogItems = backlogItems;
            _divisionProjectStatuses = divisionProjectStatuses;
            _weeklyReportStatuses = weeklyReportStatuses;
            _projectAccesses = projectAccesses;
            _doDReports = doDReports;
            _qualityReports = qualityReports;

            ValidateDomain(userRoleList);
        }

        public Project(int id, string name, string code, string division, string keyAccountManager,
            DateTime projectStartDate,
            bool phrRequired, bool dmrRequired, bool dodRequired, int projectStateTypeId,
            List<(string, string)> userRoleList, DateTime createdDate, string deliveryResponsibleName,
            string platformVersion, string jiraLink,
            string sourceCodeLink, string note,
            DateTime? projectEndDate,
            DateTime? phrRequiredFrom, DateTime? dmrRequiredFrom,
            DateTime? dmrRequiredTo) : this()
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
            
            ValidateDomain(userRoleList);
        }

        public void SetCollections(IEnumerable<QualityReport> qualityReports, IEnumerable<BacklogItem> backlogItems,
            IEnumerable<Status> statuses)
        {
            _qualityReports = qualityReports.ToList();
            _backlogItems = backlogItems.ToList();
            _statuses = statuses.ToList();
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