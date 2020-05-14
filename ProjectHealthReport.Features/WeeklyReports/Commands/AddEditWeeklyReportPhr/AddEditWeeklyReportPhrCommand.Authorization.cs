using System.Collections.Generic;
using ProjectHealthReport.Domains.Helpers;
using ResponsibilityChain.Business.AuthorizationConfigs;

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
    }
}