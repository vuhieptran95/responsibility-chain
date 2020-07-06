using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.Projects.Commands
{
    public class MarkProjectsMissedDeadlineCommand : Request<int>
    {
        public MarkProjectsMissedDeadlineCommand()
        {
            var currentYearWeek = TimeHelper.GetYearWeek(DateTime.Now);
            MissedDeadlineYearWeek = MissedDeadlineYearWeek == 0 ? TimeHelper.GetLastYearWeek(currentYearWeek) : MissedDeadlineYearWeek;
        }

        public int MissedDeadlineYearWeek { get; set; }
        
        public class Handler: IExecution<MarkProjectsMissedDeadlineCommand, int>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IOptions<AuthorizationRules> _rules;

            public Handler(ReportDbContext dbContext, IOptions<AuthorizationRules> rules)
            {
                _dbContext = dbContext;
                _rules = rules;
            }
            public async Task HandleAsync(MarkProjectsMissedDeadlineCommand request)
            {
                var currentYearWeek = TimeHelper.GetNextYearWeek(request.MissedDeadlineYearWeek);
                var date = TimeHelper.GetDate(TimeHelper.GetIsoDayOfWeek(_rules.Value.PMsCanOnlyEditTheirReportsTill.Day),
                    currentYearWeek);
                date = date.AddHours(_rules.Value.PMsCanOnlyEditTheirReportsTill.Hour);

                if (DateTime.Now <= date)
                {
                    throw new InvalidOperationException(
                        "Projects can only be marked as missed deadline after the deadline!");
                }

                var projectIdsAlreadySubmitted = _dbContext.Statuses
                    .Where(s => s.YearWeek == request.MissedDeadlineYearWeek).Select(s => s.ProjectId);

                var firstDayOfMissedDeadlineYearWeek = TimeHelper.GetFirstWorkingDateOfWeek(
                    TimeHelper.CalculateYear(request.MissedDeadlineYearWeek),
                    TimeHelper.CalculateWeek(request.MissedDeadlineYearWeek));
            
                var projectIdsJustGotEnabledPhr = _dbContext.Projects
                    .Where(p => p.PhrRequired && p.PhrRequiredFrom >= firstDayOfMissedDeadlineYearWeek)
                    .Select(p => p.Id);

                var listWeeklyReportStatuses = (await _dbContext.Projects
                        .Where(p => p.PhrRequired)
                        .Select(p => p.Id)
                        .Except(projectIdsAlreadySubmitted)
                        .Except(projectIdsJustGotEnabledPhr)
                        .ToListAsync())
                    .Select(projectId => new WeeklyReportStatus(0, projectId, request.MissedDeadlineYearWeek, true));

                await _dbContext.WeeklyReportStatuses.AddRangeAsync(listWeeklyReportStatuses);
                await _dbContext.SaveChangesAsync();

                request.Response = 1;
            }
        }
    }
}