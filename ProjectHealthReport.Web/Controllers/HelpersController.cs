using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectHealthReport.Features.Helpers;
using ResponsibilityChain.Business;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpersController : Controller
    {
        private readonly IMediator _mediator;
        private readonly RequestContext _requestContext;

        public HelpersController(IMediator mediator, RequestContext requestContext)
        {
            _mediator = mediator;
            _requestContext = requestContext;
        }

        [HttpGet("project-states")]
        public async Task<ActionResult> GetProjectStateTypes()
        {
            var dto = await _mediator.SendAsync(new GetProjectStateTypesQuery());

            return Ok(dto);
        }
        
        [HttpGet("divisions")]
        public async Task<ActionResult> GetDivisionNames()
        {
            var dto = await _mediator.SendAsync(new GetDivisionNamesQuery());

            return Ok(dto);
        }
        
        [HttpGet("allowed-weeks/{selectedYear}")]
        public async Task<ActionResult> GetAllowedWeeksOfYear([FromRoute]int selectedYear)
        {
            var listOfWeeks = await _mediator.SendAsync(new GetAllowedWeeksOfYearQuery()
                {SelectedYear = selectedYear});

            return Ok(new {AllowedWeeks = listOfWeeks});
        }
        
        [HttpGet("user-emails")]
        public async Task<ActionResult> GetAllUserEmails()
        {
            var nitecans = await _mediator.SendAsync(new GetUserEmailsQuery());
            return Ok(nitecans);
        }

        [HttpGet("allowed-yearweeks/{fromYear}")]
        public async Task<ActionResult> GetAllowedYearWeeks(int fromYear)
        {
            var dto = await _mediator.SendAsync(new GetAllowedYearWeeksQuery(){FromYear = fromYear});

            return Ok(dto);
        }

        [HttpGet("current-user")]
        public ActionResult GetCurrentUser()
        {
            return Ok(_requestContext);
        }

        [HttpGet("authorization-config")]
        public async Task<ActionResult> Method()
        {
            var dto = await _mediator.SendAsync(new GetRequestAuthorizationConfigs());

            return Ok(dto);
        }
    }
}