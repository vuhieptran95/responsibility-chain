using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.DoDs.GetDoDReport
{
    public class GetDoDReportLinksQuery : IRequest<GetDoDReportLinksQuery.Dto>
    {
        public int ProjectId { get; set; }
        public int YearWeek { get; set; }
        public int NumberOfWeek { get; set; }

        public class Handler : ExecutionHandlerBase<GetDoDReportLinksQuery, Dto>
        {
            private readonly ReportDbContext _dbContext;

            public Handler(ReportDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public override async Task HandleAsync(GetDoDReportLinksQuery request)
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

                request.Response = new Dto(){ReportLinks = reportLinks.OrderByDescending(r => r.YearWeek)};
            }
        }
        
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

        public Dto Response { get; set; }
    }

    
}