using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Features.Common.Authorization;
using ResponsibilityChain.Business.AuthorizationConfigs;
using ResponsibilityChain.Business.Authorizations;

namespace ProjectHealthReport.Features.WeeklyReports.Queries.GetWeeklyReportPhr
{
    public partial class GetWeeklyReportPhrQuery
    {
        public class Config : IAuthorizationConfig<GetWeeklyReportPhrQuery>
        {
            public List<(string[] Resources, string[] Actions)> GetAccessRights()
            {
                return new List<(string[] Resources, string[] Actions)>()
                {
                    (new[] {Resources.Project}, new[] {Actions.Read}),
                    (new[] {Resources.BacklogItem}, new[] {Actions.Read}),
                    (new[] {Resources.ProjectStatus}, new[] {Actions.Read}),
                    (new[] {Resources.QualityReport}, new[] {Actions.Read}),
                    (new[] {Resources.DoDReport}, new[] {Actions.Read}),
                };
            }
        }

        public class Authorization : AuthorizationHandler<GetWeeklyReportPhrQuery, Dto>
        {
            public Authorization(IsCoo<GetWeeklyReportPhrQuery, Dto> isCoo, IsPmo<GetWeeklyReportPhrQuery, Dto> isPmo,
                Rules rules, PreAuthorizationThrowException<GetWeeklyReportPhrQuery, Dto> throwException)
            {
                AddHandler(isCoo);
                AddHandler(isPmo);
                AddHandler(rules);
                AddHandler(throwException);
            }
        }

        public class Rules : IPreAuthorizationRule<GetWeeklyReportPhrQuery, Dto>
        {
            public async Task HandleAsync(GetWeeklyReportPhrQuery request)
            {
                if (request.AuthorizationIsHandled)
                {
                    await Task.CompletedTask;
                    return;
                }

                var role = request.RequestContext.UserRole;
                var name = request.RequestContext.Username;

                switch (role, name)
                {
                    case (AuthorizationHelper.RolePic, _):
                        request.ResourceFilter = project => project.DeliveryResponsibleName == name;
                        request.AuthorizationIsHandled = true;
                        return;
                    
                    case (AuthorizationHelper.RoleProjectManager, _):
                        request.ResourceFilter = project => project.DeliveryResponsibleName == name;
                        request.AuthorizationIsHandled = true;
                        return;

                    case (AuthorizationHelper.RoleDeliveryManager, _):
                        var division = AuthorizationHelper.DeliveryManagers[name];
                        request.ResourceFilter = p => p.Division == division;
                        request.AuthorizationIsHandled = true;
                        return;
                }
            }
        }
    }
}