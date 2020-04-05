using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectHealthReport.Features.Projects.Commands;
using ProjectHealthReport.Features.Projects.Queries.GetProject;
using ProjectHealthReport.Features.Projects.Queries.GetProjects;
using ResponsibilityChain.Business;

namespace ProjectHealthReport.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : Controller
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetProjects()
        {
            var dto = await _mediator.SendAsync(new GetProjectsQuery());

            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProject([FromRoute]int id)
        {
            var dto = await _mediator.SendAsync(new GetProjectQuery(){ProjectId = id});

            return Ok(dto);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveProject([FromRoute]int id)
        {
            var dto = await _mediator.SendAsync(new RemoveProjectCommand(){ProjectId = id});

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> AddProjectDmr([FromBody] AddProjectCommand command)
        {
            var dto = await _mediator.SendAsync(command);

            return Created("", dto);
        }

        [HttpPut("master-data")]
        public async Task<ActionResult> EditProjectMasterData([FromBody] EditProjectMasterDataCommand command)
        {
            var dto = await _mediator.SendAsync(command);

            return Ok(dto);
        }
        
        
    }
}