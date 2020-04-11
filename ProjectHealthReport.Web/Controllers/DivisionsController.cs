using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectHealthReport.Features.Divisions.Commands;
using ProjectHealthReport.Features.Divisions.Queries;
using ProjectHealthReport.Web.Helpers;
using ResponsibilityChain.Business;

namespace ProjectHealthReport.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionFilter]
    public class DivisionsController : Controller
    {
        private readonly IMediator _mediator;

        public DivisionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("index")]
        public async Task<ActionResult> GetDivisionIndex()
        {
            var dto = await _mediator.SendAsync(new GetDivisionIndexQuery());

            return Ok(dto);
        }

        [HttpGet("{name}/{yearWeek}")]
        public async Task<ActionResult> GetDivisionWeeklyReport(string name, int yearWeek)
        {
            var dto = await _mediator.SendAsync(new GetDivisionWeeklyReportQuery()
                {DivisionName = name, SelectedYearWeek = yearWeek});

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> AddEditDivisonWeeklyReport([FromBody] AddEditDivisionWeeklyReportCommand command)
        {
            var dto = await _mediator.SendAsync(command);

            return Ok(dto);
        }
    }
}