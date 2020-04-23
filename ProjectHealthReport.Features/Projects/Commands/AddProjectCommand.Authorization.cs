using System.Collections.Generic;
using ProjectHealthReport.Domains.Helpers;
using ResponsibilityChain.Business.AuthorizationConfigs;

namespace ProjectHealthReport.Features.Projects.Commands
{
    public partial class AddProjectCommand
    {
        public class AuthorizationConfig : IAuthorizationConfig<AddProjectCommand>
        {
            public List<(string[] Resources, string[] Actions)> GetAccessRights()
            {
                return new List<(string[] Resources, string[] Actions)>()
                {
                    (new[] {Resources.Project, Resources.ProjectMaster}, new[] {Actions.Read, Actions.Create}),
                };
            }
        }
    }
}