using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectHealthReport.Domains.Exceptions;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Features.Common.Authorization;
using ResponsibilityChain.Business.AuthorizationConfigs;
using ResponsibilityChain.Business.Authorizations;

namespace ProjectHealthReport.Features.Projects.Queries.GetProjectIndexPhr
{
    public partial class GetProjectIndexPhrQuery
    {
        public class AuthorizationConfig : IAuthorizationConfig<GetProjectIndexPhrQuery>
        {
            public List<(string[] Resources, string[] Actions)> GetAccessRights()
            {
                return new List<(string[] Resources, string[] Actions)>()
                {
                    (new[] {Resources.Project},
                        new[] {Actions.Read}),
                };
            }
        }
        
        public class AuthorizationHandler: AuthorizationHandler<GetProjectIndexPhrQuery, Dto>
        {
            public AuthorizationHandler(IsCoo<GetProjectIndexPhrQuery, Dto> isCoo, IsPmo<GetProjectIndexPhrQuery, Dto> isPmo,
                Rules rules, PreAuthorizationThrowException<GetProjectIndexPhrQuery, Dto> throwException)
            {
                AddHandler(isCoo);
                AddHandler(isPmo);
                AddHandler(rules);
                AddHandler(throwException);
            }
        }
        
        public class Rules : IPreAuthorizationRule<GetProjectIndexPhrQuery, Dto>
        {
            public async Task HandleAsync(GetProjectIndexPhrQuery request)
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