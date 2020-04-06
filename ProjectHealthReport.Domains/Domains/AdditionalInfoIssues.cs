using System;
using ProjectHealthReport.Domains.Exceptions;
using ProjectHealthReport.Domains.Helpers;

namespace ProjectHealthReport.Domains.Domains
{
    public class AdditionalInfoIssues
    {
        public AdditionalInfoIssues()
        {
            
        }
        public AdditionalInfoIssues(int additionalInfoId, int issueId, string status, AdditionalInfo additionalInfo,
            Issue issue)
        {
            AdditionalInfoId = additionalInfoId;
            IssueId = issueId;
            Status = status;
            AdditionalInfo = additionalInfo;
            Issue = issue;
            
            ValidateStatus();
        }

        public int AdditionalInfoId { get; private set; }
        public int IssueId { get; private set; }
        public string Status { get; private set; }
        public AdditionalInfo AdditionalInfo { get; private set; }
        public Issue Issue { get; private set; }

        public void SetStatus(string status)
        {
            Status = status;
            ValidateStatus();
        }

        public void ValidateStatus()
        {
            if (Status != MiscHelper.IssueStatusOpen || Status != MiscHelper.IssueStatusClosed)
            {
                DomainExceptionCode.Throw(DomainError.D021, this);
            }
        }
    }
}