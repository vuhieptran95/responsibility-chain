using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ProjectHealthReport.Domains.Exceptions;
using ProjectHealthReport.Domains.Helpers;

namespace ProjectHealthReport.Domains.Domains
{
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

        public void OrganizationInfoMustBeValid(List<(string User, string Role)> userRoleList)
        {
            if (!AuthorizationHelper.DeliveryManagers.Select(i => i.Value).Contains(Division))
            {
                DomainExceptionCode.Throw(DomainError.D002, this);
            }

            if (!userRoleList.Where(i => i.Role == AuthorizationHelper.RoleKam).Select(i => i.User)
                .Contains(KeyAccountManager))
            {
                DomainExceptionCode.Throw(DomainError.D003, this);
            }

            if (DeliveryResponsibleName != null && !userRoleList.Where(i => i.Role == AuthorizationHelper.RolePic)
                .Select(i => i.User)
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

        public void ValidateBacklogItems()
        {
            var itemsRemaining = BacklogItems.Sum(b => b.ItemsAdded - b.ItemsDone);
            if (itemsRemaining < 0)
            {
                DomainExceptionCode.Throw(DomainError.D036, this);
            }

            var storyPointsRemaining = BacklogItems.Sum(b => b.StoryPointsAdded - b.StoryPointsDone);
            if (storyPointsRemaining < 0)
            {
                DomainExceptionCode.Throw(DomainError.D037, this);
            }
        }

        public void ValidateQualityReport()
        {
            var remainingBugs = QualityReports.Sum(q => q.CriticalBugs + q.MajorBugs + q.MinorBugs - q.DoneBugs);
            if (remainingBugs < 0)
            {
                DomainExceptionCode.Throw(DomainError.D038, this);
            }
        }

    }
}