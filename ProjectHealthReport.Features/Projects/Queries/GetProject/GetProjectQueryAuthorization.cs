using System;
using System.Linq;
using System.Threading.Tasks;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Features.Common.Authorization;
using ProjectHealthReport.Features.Projects.Queries.GetProjectsCaching;
using ResponsibilityChain.Business;
using ResponsibilityChain.Business.AuthorizationConfigs;
using ResponsibilityChain.Business.Authorizations;

namespace ProjectHealthReport.Features.Projects.Queries.GetProjects
{
    public partial class GetProjectQuery
    {
        public class AuthorizationHandler : AuthorizationHandler<GetProjectQuery, Dto>
        {
            public AuthorizationHandler(
                IsCoo<GetProjectQuery, Dto> isCoo,
                IsPmo<GetProjectQuery, Dto> isPmo,
                Rules rules,
                PreAuthorizationThrowException<GetProjectQuery, Dto> throwException)
            {
                AddHandler(isCoo);
                AddHandler(isPmo);
                AddHandler(rules);
                AddHandler(throwException);
            }
        }

        public class Rules : IPreAuthorizationRule<GetProjectQuery, Dto>
        {
            private readonly IMediator _mediator;

            public Rules(IMediator mediator)
            {
                _mediator = mediator;
            }

            public async Task HandleAsync(GetProjectQuery request)
            {
                if (request.AuthorizationIsHandled)
                {
                    await Task.CompletedTask;
                    return;
                }

                var projects = (await _mediator.SendAsync(new GetProjectsCachingQuery())).Projects;
                var project = projects.First(p => p.Id == request.ProjectId);

                var role = request.RequestContext.UserRole;
                var name = request.RequestContext.Username;

                switch (role, name)
                {
                    case (AuthorizationHelper.RolePic, _):
                    case (AuthorizationHelper.RoleProjectManager, _):
                        if (project.Pics.Contains(name.ToUpper()))
                        {
                            request.AuthorizationIsHandled = true;
                        }
                        else
                        {
                            throw new UnauthorizedAccessException("You are not authorized to view this project");
                        }

                        return;

                    case (AuthorizationHelper.RoleDeliveryManager, _):
                        if (project.Division == AuthorizationHelper.DeliveryManagers[name])
                        {
                            request.AuthorizationIsHandled = true;
                        }
                        else
                        {
                            throw new UnauthorizedAccessException("You are not authorized to view this project");
                        }

                        return;
                }
            }
        }
    }
}