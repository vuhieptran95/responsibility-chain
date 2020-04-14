using System.Threading.Tasks;
using ProjectHealthReport.Features.Projects.Queries.GetProject;
using ProjectHealthReport.Features.WeeklyReports.Queries.GetWeeklyReportPhr;
using ResponsibilityChain;
using ResponsibilityChain.Business;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.WeeklyReports.Queries.GetGeneratedWeeklyReportPhr
{
    public class GetGeneratedWeeklyReportPhrQuery : IRequest<GetGeneratedWeeklyReportPhrQuery.Dto>
    {
        public int ProjectId { get; set; }
        public int Year { get; set; }
        public int Week { get; set; }
        public int NumberOfWeek { get; set; } = 4;
        public int NumberOfWeekNotShowClosedItem { get; set; } = 2;
        
        public class Handler : ExecutionHandlerBase<GetGeneratedWeeklyReportPhrQuery, Dto>
        {
            private readonly IMediator _mediator;

            public Handler(IMediator mediator)
            {
                _mediator = mediator;
            }
            public override async Task HandleAsync(GetGeneratedWeeklyReportPhrQuery request)
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

        public Dto Response { get; set; }
    }
}