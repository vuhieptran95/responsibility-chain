using System.Threading.Tasks;
using ProjectHealthReport.Features.Projects.Queries.GetProjects;
using ProjectHealthReport.Features.WeeklyReports.Queries.GetWeeklyReportPhr;
using ResponsibilityChain;
using ResponsibilityChain.Business;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.WeeklyReports.Queries.GetGeneratedWeeklyReportPhr
{
    public class GetGeneratedWeeklyReportPhrQuery : Request<GetGeneratedWeeklyReportPhrQuery.Dto>
    {
        public int ProjectId { get; set; }
        public int Year { get; set; }
        public int Week { get; set; }
        public int NumberOfWeek { get; set; } = 4;
        public int NumberOfWeekNotShowClosedItem { get; set; } = 2;

        public class Handler : IExecution<GetGeneratedWeeklyReportPhrQuery, Dto>
        {
            private readonly IMediator _mediator;

            public Handler(IMediator mediator)
            {
                _mediator = mediator;
            }

            public async Task HandleAsync(GetGeneratedWeeklyReportPhrQuery request)
            {
                var getProjectQuery = new GetProjectQuery {ProjectId = request.ProjectId};
                var getWeeklyReportQuery = new GetWeeklyReportPhrQuery()
                {
                    ProjectId = request.ProjectId,
                    NumberOfWeek = request.NumberOfWeek,
                    Week = request.Week,
                    Year = request.Year,
                    NumberOfWeekNotShowClosedItem = request.NumberOfWeekNotShowClosedItem
                };

                var weeklyReport = await _mediator.SendAsync(getWeeklyReportQuery);

                if (weeklyReport.BacklogItem != null)
                {
                    weeklyReport.BacklogItemListReadOnly.Add(weeklyReport.BacklogItem);
                }

                if (weeklyReport.QualityReport != null)
                {
                    weeklyReport.QualityReportListReadOnly.Add(weeklyReport.QualityReport);
                }

                var dto = new Dto
                {
                    Project = await _mediator.SendAsync(getProjectQuery),
                    WeeklyReport = weeklyReport
                };

                request.Response = dto;
            }
        }

        public class Dto
        {
            public GetProjectQuery.Dto Project { get; set; }
            public GetWeeklyReportPhrQuery.Dto WeeklyReport { get; set; }
        }
    }
}