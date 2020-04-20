using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ProjectHealthReport.Domains.Exceptions;
using ProjectHealthReport.Domains.Helpers;

namespace ProjectHealthReport.Domains.Domains
{
    public partial class Project
    {
        public void AddEditStatus(Status status)
        {
            // TODO Validate status data related to projects....

            if (status.Id == 0)
            {
                _statuses.Add(status);
            }
            else
            {
                var statusInDb = _statuses.First(s => s.Id == status.Id);
                statusInDb.UpdateValue(status);
            }
        }

        public void AddEditBacklogItem(BacklogItem item)
        {
            // TODO Validate BacklogItem data
            
            if (item.Id == 0)
            {
                _backlogItems.Add(item);
            }
            else
            {
                var itemInDb = _backlogItems.First(b => b.Id == item.Id);
                itemInDb.UpdateValue(item);
            }
        }
        
        public void AddEditQualityReport(QualityReport item)
        {
            // TODO Validate Quality Report data
            
            if (item.Id == 0)
            {
                _qualityReports.Add(item);
            }
            else
            {
                var itemInDb = _qualityReports.First(b => b.Id == item.Id);
                itemInDb.UpdateValue(item);
            }
        }

        public void AddEditDivisionStatus(DivisionProjectStatus status)
        {
            // TODO Validate Division Project Status data
            
            if (status.Id == 0)
            {
                _divisionProjectStatuses.Add(status);
            }
            else
            {
                var statusInDb = _divisionProjectStatuses.First(s => s.Id == status.Id);
                statusInDb.UpdateValue(status);
            }
        }
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
            if (Regex.IsMatch(Code, "^[a-zA-Z0-9]+$") && Code.Length == 3 && Code.ToUpper() == Code)
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

            if (DeliveryResponsibleName != null && !userRoleList.Where(i => i.Item2 == AuthorizationHelper.RolePic)
                .Select(i => i.Item1)
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
            if (ProjectEndDate.HasValue && ProjectEndDate.Value < ProjectStartDate)
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

            if (PhrRequired && string.IsNullOrEmpty(DeliveryResponsibleName))
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