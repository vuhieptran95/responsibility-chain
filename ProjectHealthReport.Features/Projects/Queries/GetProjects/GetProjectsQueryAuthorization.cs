using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Features.Common.Authorization;
using ResponsibilityChain;
using ResponsibilityChain.Business.AuthorizationConfigs;
using ResponsibilityChain.Business.Authorizations;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.Projects.Queries.GetProjects
{
    public class GetProjectsQueryAuthorization : AuthorizationHandler<GetProjectsQuery, GetProjectsQuery.Dto>
    {
        public class Config : IAuthorizationConfig<GetProjectsQuery>
        {
            public List<(string[] Resources, string[] Actions)> GetAccessRights()
            {
                return new List<(string[] Resources, string[] Actions)>()
                {
                    (new[] {Resources.Project, Resources.ProjectAdmin},
                        new[] {Actions.Read}),
                };
            }
        }
        
        public GetProjectsQueryAuthorization(
            IsCoo<GetProjectsQuery, GetProjectsQuery.Dto> isCoo,
            IsPmo<GetProjectsQuery, GetProjectsQuery.Dto> isPmo,
            Rules rules,
            PreAuthorizationThrowException<GetProjectsQuery, GetProjectsQuery.Dto> throwException)
        {
            AddHandler(isCoo);
            AddHandler(isPmo);
            AddHandler(rules);
            AddHandler(throwException);
        }

        public class Rules : IPreAuthorization<GetProjectsQuery, GetProjectsQuery.Dto>
        {
            public async Task HandleAsync(GetProjectsQuery request)
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
                    case (AuthorizationHelper.RolePic, "PM1"):
                        request.ResourceFilter = project =>
                            project.DeliveryResponsibleName == "PM1" || project.DeliveryResponsibleName == "PM2";
                        request.AuthorizationIsHandled = true;
                        return;
                    
                    case (AuthorizationHelper.RolePic, _):
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