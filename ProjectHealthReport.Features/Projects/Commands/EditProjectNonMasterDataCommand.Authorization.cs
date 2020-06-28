using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Features.Common.Authorization;
using ProjectHealthReport.Features.Projects.Queries.GetProjectsCaching;
using ResponsibilityChain.Business;
using ResponsibilityChain.Business.AuthorizationConfigs;
using ResponsibilityChain.Business.Authorizations;

namespace ProjectHealthReport.Features.Projects.Commands
{
    public partial class EditProjectNonMasterDataCommand
    {
        public class AuthorizationConfig : IAuthorizationConfig<EditProjectMasterDataCommand>
        {
            public List<(string[] Resources, string[] Actions)> GetAccessRights()
            {
                return new List<(string[] Resources, string[] Actions)>()
                {
                    (new[] {Resources.Project, Resources.ProjectNonMaster},
                        new[] {Actions.Read, Actions.Create, Actions.Update}),
                };
            }
        }

        public class AuthorizationHandler : AuthorizationHandler<EditProjectNonMasterDataCommand, int>
        {
            public AuthorizationHandler(
                IsCoo<EditProjectNonMasterDataCommand, int> isCoo,
                IsPmo<EditProjectNonMasterDataCommand, int> isPmo,
                Rules rules,
                PreAuthorizationThrowException<EditProjectNonMasterDataCommand, int> throwException)
            {
                AddHandler(isCoo);
                AddHandler(isPmo);
                AddHandler(rules);
                AddHandler(throwException);
            }
        }

        public class Rules : IPreAuthorizationRule<EditProjectNonMasterDataCommand, int>
        {
            private readonly IMediator _mediator;

            public Rules(IMediator mediator)
            {
                _mediator = mediator;
            }

            public async Task HandleAsync(EditProjectNonMasterDataCommand request)
            {
                if (request.AuthorizationIsHandled)
                {
                    await Task.CompletedTask;
                    return;
                }

                var projects = (await _mediator.SendAsync(new GetProjectsCachingQuery())).Projects;
                var project = projects.First(p => p.Id == request.Id);

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
                            throw new UnauthorizedAccessException("You are not authorized to edit this project");
                        }

                        return;

                    case (AuthorizationHelper.RoleDeliveryManager, _):
                        if (project.Division == AuthorizationHelper.DeliveryManagers[name])
                        {
                            request.AuthorizationIsHandled = true;
                        }
                        else
                        {
                            throw new UnauthorizedAccessException("You are not authorized to edit this project");
                        }

                        return;
                }
            }
        }
    }
}