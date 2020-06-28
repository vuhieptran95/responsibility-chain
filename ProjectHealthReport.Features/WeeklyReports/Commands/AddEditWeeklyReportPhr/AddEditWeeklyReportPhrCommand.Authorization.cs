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

namespace ProjectHealthReport.Features.WeeklyReports.Commands.AddEditWeeklyReportPhr
{
    public partial class AddEditWeeklyReportPhrCommand
    {
        public class Config : IAuthorizationConfig<AddEditWeeklyReportPhrCommand>
        {
            public List<(string[] Resources, string[] Actions)> GetAccessRights()
            {
                return new List<(string[] Resources, string[] Actions)>()
                {
                    (new[] {Resources.Project}, new[] {Actions.Read}),
                    (new[] {Resources.BacklogItem}, new[] {Actions.Read, Actions.Create, Actions.Update}),
                    (new[] {Resources.ProjectStatus}, new[] {Actions.Read, Actions.Create, Actions.Update}),
                    (new[] {Resources.QualityReport}, new[] {Actions.Read, Actions.Create, Actions.Update}),
                    (new[] {Resources.DoDReport}, new[] {Actions.Read, Actions.Create, Actions.Update}),
                };
            }
        }
        
        public class AuthorizationHandler : AuthorizationHandler<AddEditWeeklyReportPhrCommand, int>
        {
            public AuthorizationHandler(
                IsCoo<AddEditWeeklyReportPhrCommand, int> isCoo,
                IsPmo<AddEditWeeklyReportPhrCommand, int> isPmo,
                Rules rules,
                PreAuthorizationThrowException<AddEditWeeklyReportPhrCommand, int> throwException)
            {
                AddHandler(isCoo);
                AddHandler(isPmo);
                AddHandler(rules);
                AddHandler(throwException);
            }
        }
        
        public class Rules : IPreAuthorizationRule<AddEditWeeklyReportPhrCommand, int>
        {
            private readonly IMediator _mediator;

            public Rules(IMediator mediator)
            {
                _mediator = mediator;
            }
            public async Task HandleAsync(AddEditWeeklyReportPhrCommand request)
            {
                if (request.AuthorizationIsHandled)
                {
                    await Task.CompletedTask;
                    return;
                }

                var projects = (await _mediator.SendAsync(new GetProjectsCachingQuery())).Projects;
                var project = projects.First(p => p.Id == request.Report.ProjectId);

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
                            throw new UnauthorizedAccessException("You are not authorized to edit this project report");
                        }

                        return;

                    case (AuthorizationHelper.RoleDeliveryManager, _):
                        if (project.Division == AuthorizationHelper.DeliveryManagers[name])
                        {
                            request.AuthorizationIsHandled = true;
                        }
                        else
                        {
                            throw new UnauthorizedAccessException("You are not authorized to edit this project report");
                        }

                        return;
                }
            }
        }
    }
}