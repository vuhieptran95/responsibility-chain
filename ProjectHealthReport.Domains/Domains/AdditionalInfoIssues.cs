using System;
using ProjectHealthReport.Domains.Exceptions;
using ProjectHealthReport.Domains.Helpers;

namespace ProjectHealthReport.Domains.Domains
{
    public class AdditionalInfoIssues
    {
        private Issue _issue;
        private AdditionalInfo _additionalInfo;
        private string _status;
        private int _issueId;
        private int _additionalInfoId;

        public AdditionalInfoIssues()
        {
            
        }
        public AdditionalInfoIssues(int additionalInfoId, int issueId, string status, AdditionalInfo additionalInfo,
            Issue issue)
        {
            _additionalInfoId = additionalInfoId;
            _issueId = issueId;
            _status = status;
            _additionalInfo = additionalInfo;
            _issue = issue;
            
            ValidateStatus();
        }

        public AdditionalInfoIssues(string status, int issueId, int additionalInfoId)
        {
            _status = status;
            _issueId = issueId;
            _additionalInfoId = additionalInfoId;
        }

        public int AdditionalInfoId => _additionalInfoId;

        public int IssueId => _issueId;

        public string Status => _status;

        public AdditionalInfo AdditionalInfo => _additionalInfo;

        public Issue Issue => _issue;

        public void SetStatus(string status)
        {
            _status = status;
            ValidateStatus();
        }

        public void ValidateStatus()
        {
            if (Status != MiscHelper.IssueStatusOpen && Status != MiscHelper.IssueStatusClosed)
            {
                DomainExceptionCode.Throw(DomainError.D021, this);
            }
        }
    }
}