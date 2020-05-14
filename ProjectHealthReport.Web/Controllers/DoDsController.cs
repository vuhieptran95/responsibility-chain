using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectHealthReport.Features.DoDs.AddEditDoDReport;
using ProjectHealthReport.Features.DoDs.GetDoDReport;
using ProjectHealthReport.Features.DoDs.Metrics.EditMetrics;
using ProjectHealthReport.Features.DoDs.Metrics.GetMetrics;
using ProjectHealthReport.Features.DoDs.Metrics.GetThresholds;
using ProjectHealthReport.Features.DoDs.Metrics.RemoveMetrics;
using ProjectHealthReport.Web.Helpers;
using ResponsibilityChain.Business;

namespace ProjectHealthReport.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionFilter]
    public class DoDsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DoDsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        [HttpGet("thresholds")]
        public async Task<ActionResult> GetThresholds()
        {
            var dto = await _mediator.SendAsync(new GetThresholdsQuery());
            return Ok(dto);
        }
        
        [HttpGet("links")]
        public async Task<ActionResult> GetReportLinks([FromQuery]GetDoDReportLinksQuery query)
        {
            var dto = await _mediator.SendAsync(query);
            return Ok(dto);
        }
        
        [HttpPost("links")]
        public async Task<ActionResult> AddEditReportLinks(AddEditDoDReportLinkCommand command)
        {
            var dto = await _mediator.SendAsync(command);
            return Ok(dto);
        }
        
        [HttpGet("metrics")]
        public async Task<ActionResult> GetMetrics()
        {
            var dto = await _mediator.SendAsync(new GetMetricsQuery());
            return Ok(dto);
        }
        
        [HttpPost("metrics")]
        public async Task<ActionResult> AddMetric(AddMetricCommand command)
        {
            var metricId = await _mediator.SendAsync(command);
            return Created("", metricId);
        }
        
        [HttpPut("metrics")]
        public async Task<ActionResult> EditMetrics(EditMetricsCommand command)
        {
            await _mediator.SendAsync(command);
            return Ok();
        }
        
        [HttpDelete("metrics/tool/{tool}")]
        public async Task<ActionResult> RemoveMetrics(string tool)
        {
            await _mediator.SendAsync(new RemoveMetricsCommand(){Tool = tool});
            return Ok();
        }
        
        [HttpDelete("metrics/{metricId}")]
        public async Task<ActionResult> RemoveMetric(int metricId)
        {
            await _mediator.SendAsync(new RemoveMetricCommand(){MetricId = metricId});
            return Ok();
        }

        [HttpPut("dod-reports")]
        public async Task<ActionResult> EditDodReport([FromBody] EditDoDReportCommand command)
        {
            await _mediator.SendAsync(command);

            return Ok();
        }
    }
}