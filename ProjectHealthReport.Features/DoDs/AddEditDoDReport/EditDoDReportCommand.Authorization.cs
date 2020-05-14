using System.Collections.Generic;
using ProjectHealthReport.Domains.Helpers;
using ResponsibilityChain.Business.AuthorizationConfigs;

namespace ProjectHealthReport.Features.DoDs.AddEditDoDReport
{
    public partial class EditDoDReportCommand
    {
        public class AuthorizationConfig : IAuthorizationConfig<EditDoDReportCommand>
        {
            public List<(string[] Resources, string[] Actions)> GetAccessRights()
            {
                return new List<(string[] Resources, string[] Actions)>()
                {
                    (new[] {Resources.DoDReport}, new[] {Actions.Read, Actions.Create, Actions.Update}),
                    (new[] {Resources.Project}, new[] {Actions.Read})
                };
            }
        }
    }
}