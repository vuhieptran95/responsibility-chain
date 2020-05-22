using System.Collections.Generic;
using ProjectHealthReport.Domains.Helpers;
using ResponsibilityChain.Business.AuthorizationConfigs;

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
    }
}