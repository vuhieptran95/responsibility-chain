using System.Collections.Generic;
using ProjectHealthReport.Domains.Helpers;
using ResponsibilityChain.Business.AuthorizationConfigs;

namespace ProjectHealthReport.Features.Projects.Queries.GetProjects
{
    public partial class GetProjectsNotSubmitReportQuery
    {
        public class AuthorizationConfig : IAuthorizationConfig<GetProjectsNotSubmitReportQuery>
        {
            public List<(string[] Resources, string[] Actions)> GetAccessRights()
            {
                return new List<(string[] Resources, string[] Actions)>()
                {
                    (new[] {Resources.ProjectsNotYetSubmit},
                        new[] {Actions.Read}),
                };
            }
        }
    }
}