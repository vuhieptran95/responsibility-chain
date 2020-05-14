using System.Collections.Generic;
using ProjectHealthReport.Domains.Helpers;
using ResponsibilityChain.Business.AuthorizationConfigs;

namespace ProjectHealthReport.Features.WeeklyReports.Queries.GetWeeklyReportPhr
{
    public partial class GetWeeklyReportPhr
    {
        public class Config : IAuthorizationConfig<GetWeeklyReportPhrQuery>
        {
            public List<(string[] Resources, string[] Actions)> GetAccessRights()
            {
                return new List<(string[] Resources, string[] Actions)>()
                {
                    (new[] {Resources.Project}, new[] {Actions.Read}),
                    (new[] {Resources.BacklogItem}, new[] {Actions.Read}),
                    (new[] {Resources.ProjectStatus}, new[] {Actions.Read}),
                    (new[] {Resources.QualityReport}, new[] {Actions.Read}),
                    (new[] {Resources.DoDReport}, new[] {Actions.Read}),
                };
            }
        }
    }
}