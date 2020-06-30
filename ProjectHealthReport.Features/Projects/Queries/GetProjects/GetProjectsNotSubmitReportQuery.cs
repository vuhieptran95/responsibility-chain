using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ResponsibilityChain.Business.AuthorizationConfigs;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.Projects.Queries.GetProjects
{
    public partial class GetProjectsNotSubmitReportQuery : Request<GetProjectsNotSubmitReportQuery.Dto>
    {
        public int SelectedYearWeek { get; set; }

        public class Dto
        {
            public IEnumerable<ProjectDto> Projects { get; set; }
            public DateTime Deadline { get; set; }
        }

        public class ProjectDto
        {
            public int ProjectId { get; set; }
            public string ProjectName { get; set; }
            public int SelectedYearWeek { get; set; }
            public string PicEmail { get; set; }
            public string DmEmail { get; set; }
            public string DivisionName { get; set; }
        }

        public class Handler : IExecution<GetProjectsNotSubmitReportQuery, Dto>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IOptions<AuthorizationRules> _rule;

            public Handler(ReportDbContext dbContext, IOptions<AuthorizationRules> rule)
            {
                _dbContext = dbContext;
                _rule = rule;
            }

            public async Task HandleAsync(GetProjectsNotSubmitReportQuery request)
            {
                var firstWorkingDayOfWeek = TimeHelper.GetFirstWorkingDateOfWeek(
                    TimeHelper.CalculateYear(request.SelectedYearWeek),
                    TimeHelper.CalculateWeek(request.SelectedYearWeek));
                var listProjectDto = await _dbContext.Projects
                    .Where(p => p.PhrRequired)
                    .Where(p => p.Statuses.SingleOrDefault(s => s.YearWeek == request.SelectedYearWeek) == null)
                    .Where(p => p.PhrRequiredFrom < firstWorkingDayOfWeek)
                    .Select(p => new ProjectDto()
                    {
                        ProjectId = p.Id,
                        ProjectName = p.Name,
                        SelectedYearWeek = request.SelectedYearWeek,
                        PicEmail = p.DeliveryResponsibleName + "@niteco.se",
                        DivisionName = p.Division
                    })
                    .ToListAsync();

                // listProjectDto.ForEach(p =>
                // {
                //     p.DmEmail = AuthorizationHelper.DeliveryManagers.First(m => m.Value == p.DivisionName).Key +
                //                 "@niteco.se";
                // });

                var nextYearWeek = TimeHelper.GetNextYearWeek(request.SelectedYearWeek);
                var isoDate = TimeHelper.GetIsoDayOfWeek(_rule.Value.PMsCanOnlyEditTheirReportsTill.Day);
                var date = TimeHelper.GetDate(isoDate, nextYearWeek)
                    .AddHours(_rule.Value.PMsCanOnlyEditTheirReportsTill.Hour);

                request.Response = new Dto()
                {
                    Deadline = date,
                    Projects = listProjectDto
                };
            }
        }
    }
}