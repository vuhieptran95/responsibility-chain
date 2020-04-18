using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Features.Common;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.Projects.Queries.GetProjectPhrWithReportYearWeeksAndStatuses
{
    public class
        GetProjectPhrWithReportYearWeeksAndStatusesQuery : IRequest<GetProjectPhrWithReportYearWeeksAndStatusesQuery.Dto
        >
    {
        public int ProjectId { get; set; }

        public class Dto
        {
            public IEnumerable<YearWeekStatus> YearWeekStatuses { get; set; }

            public class YearWeekStatus
            {
                public YearWeekStatus()
                {
                    Statuses = new List<string>();
                }

                public int YearWeek { get; set; }
                public List<string> Statuses { get; set; }
            }
        }

        public class Handler : IExecution<GetProjectPhrWithReportYearWeeksAndStatusesQuery, Dto>
        {
            private readonly ReportDbContext _dbContext;

            public Handler(ReportDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task HandleAsync(GetProjectPhrWithReportYearWeeksAndStatusesQuery request)
            {
                var yearWeeks = await _dbContext.Projects.Where(p => p.Id == request.ProjectId)
                    .Select(p => new
                    {
                        missedYearWeek = p.WeeklyReportStatuses.Where(ws => ws.IsDeadlineMissed == true)
                            .Select(ws => ws.YearWeek),
                        filledYearWeek = p.Statuses.Select(s => s.YearWeek)
                    })
                    .FirstOrDefaultAsync();

                var missedFilledYearWeek = yearWeeks.missedYearWeek.Intersect(yearWeeks.filledYearWeek).ToList();

                var filledOnTimeYearWeek = yearWeeks.filledYearWeek.Except(yearWeeks.missedYearWeek).ToList();

                var missedNotFillYearWeek = yearWeeks.missedYearWeek.Except(missedFilledYearWeek).ToList();

                var currentYearWeek = TimeHelper.GetYearWeek(DateTime.Now);
                var minMissedYearWeek = currentYearWeek;
                var minFilledYearWeek = currentYearWeek;

                if (yearWeeks.missedYearWeek.Any())
                {
                    minMissedYearWeek = yearWeeks.missedYearWeek.Min();
                }

                if (yearWeeks.filledYearWeek.Any())
                {
                    minFilledYearWeek = yearWeeks.filledYearWeek.Min();
                }

                var smallestYearWeek = Math.Min(minMissedYearWeek, minFilledYearWeek);

                var yearWeeksRange = TimeHelper
                    .GetListYearWeekFrom(smallestYearWeek, currentYearWeek).ToList();

                if (smallestYearWeek == currentYearWeek)
                {
                    yearWeeksRange.Insert(0, smallestYearWeek);
                }
                else if (smallestYearWeek < currentYearWeek)
                {
                    yearWeeksRange.Insert(0, smallestYearWeek);
                    yearWeeksRange.Add(currentYearWeek);
                }

                var yearWeekStatuses = yearWeeksRange
                    .Select(yw =>
                    {
                        var ywStatus = new Dto.YearWeekStatus()
                        {
                            YearWeek = yw
                        };

                        if (missedFilledYearWeek.Contains(yw))
                        {
                            ywStatus.Statuses = new List<string>
                                {PhrHelper.MissedStatus, PhrHelper.FilledStatus};
                        }
                        else if (filledOnTimeYearWeek.Contains(yw))
                        {
                            ywStatus.Statuses = new List<string>()
                                {PhrHelper.OnTimeStatus, PhrHelper.FilledStatus};
                        }
                        else if (missedNotFillYearWeek.Contains(yw))
                        {
                            ywStatus.Statuses = new List<string>()
                                {PhrHelper.MissedStatus, PhrHelper.NotFilledStatus};
                        }
                        else
                        {
                            ywStatus.Statuses = new List<string>() {PhrHelper.NotFilledStatus};
                        }

                        return ywStatus;
                    })
                    .OrderByDescending(yws => yws.YearWeek);

                var dto = new Dto()
                {
                    YearWeekStatuses = yearWeekStatuses
                };

                request.Response = dto;
            }
        }

        public Dto Response { get; set; }
    }
}