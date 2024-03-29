﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ResponsibilityChain;
using ResponsibilityChain.Business.AuthorizationConfigs;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.DoDs.GetDoDReport
{
    public partial class GetDoDReportLinksQuery : Request<GetDoDReportLinksQuery.Dto>
    {
        public int ProjectId { get; set; }
        public int YearWeek { get; set; }
        public int NumberOfWeek { get; set; }

        public class Dto
        {
            public IEnumerable<ReportLink> ReportLinks { get; set; }

            public class ReportLink
            {
                public int YearWeek { get; set; }
                public string LinkToReport { get; set; }
                public string ReportFileName { get; set; }
            }
        }

        public class AuthorizationConfig : IAuthorizationConfig<GetDoDReportLinksQuery>
        {
            public List<(string[] Resources, string[] Actions)> GetAccessRights()
            {
                return new List<(string[] Resources, string[] Actions)>()
                {
                    (new[] {Resources.DoDReport}, new[] {Actions.Read}),
                    (new[] {Resources.Project}, new[] {Actions.Read})
                };
            }
        }
    }


    public partial class GetDoDReportLinksQuery
    {
        public class Handler : IExecution<GetDoDReportLinksQuery, Dto>
        {
            private readonly ReportDbContext _dbContext;

            public Handler(ReportDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task HandleAsync(GetDoDReportLinksQuery request)
            {
                var yearWeeksToGetDod =
                    TimeHelper.GetYearWeeksOfXRecentWeeksStartFrom(TimeHelper.CalculateYear(request.YearWeek),
                        TimeHelper.CalculateWeek(request.YearWeek), request.NumberOfWeek);

                yearWeeksToGetDod.Add(request.YearWeek);

                var reportLinks = await _dbContext.DoDReports.Where(d =>
                        d.ProjectId == request.ProjectId && yearWeeksToGetDod.Contains(d.YearWeek))
                    .Select(d => new Dto.ReportLink()
                    {
                        YearWeek = d.YearWeek,
                        LinkToReport = d.LinkToReport,
                        ReportFileName = d.ReportFileName
                    })
                    .Distinct()
                    .ToListAsync();

                var yearWeeks = reportLinks.Select(r => r.YearWeek);

                var listReportLinkToAdds = yearWeeksToGetDod.Select(yw =>
                {
                    if (!yearWeeks.Contains(yw))
                    {
                        return new Dto.ReportLink()
                        {
                            YearWeek = yw
                        };
                    }

                    return null;
                });

                reportLinks.AddRange(listReportLinkToAdds.Where(r => r != null));

                request.Response = new Dto() {ReportLinks = reportLinks.OrderByDescending(r => r.YearWeek)};
            }
        }
    }
}