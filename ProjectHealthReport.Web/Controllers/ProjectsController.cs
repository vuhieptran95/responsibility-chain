﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ProjectHealthReport.Features.Projects.Commands;
using ProjectHealthReport.Features.Projects.Queries.GetProjects;
using ProjectHealthReport.Features.Projects.Queries.GetProjectCaching;
using ProjectHealthReport.Features.Projects.Queries.GetProjectIndexPhr;
using ProjectHealthReport.Features.Projects.Queries.GetProjectPhrWithReportYearWeeksAndStatuses;
using ProjectHealthReport.Features.Projects.Queries.GetProjectsPhrWithWeeklyStatuses;
using ProjectHealthReport.Web.Helpers;
using ResponsibilityChain.Business;

namespace ProjectHealthReport.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionFilter]
    [Authorize]
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
        
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("phr/not-submit-report/year-week/{yearWeek}")]
        public async Task<ActionResult> GetProjectsNotSubmitReport(int yearWeek)
        {
            var dto = await _mediator.SendAsync(new GetProjectsNotSubmitReportQuery(){SelectedYearWeek = yearWeek});

            return Ok(dto);
        }
        
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("phr/missed-deadline/year-week/{yearWeek}")]
        public async Task<ActionResult> MarkProjectsMissedDeadline(int yearWeek)
        {
            var dto = await _mediator.SendAsync(new MarkProjectsMissedDeadlineCommand(){MissedDeadlineYearWeek = yearWeek});

            return Ok(dto);
        }

        [HttpGet("phr/project-index")]
        public async Task<ActionResult> GetProjectsIndexPhr()
        {
            var dto = await _mediator.SendAsync(new GetProjectIndexPhrQuery());
            
            return Ok(dto);
        }

        [HttpGet("phr/projects-with-weekly-status/year-week/{yearWeek}")]
        public async Task<ActionResult> GetProjectsWithWeeklyStatusQuery([FromRoute]int yearWeek)
        {
            var dto = await _mediator.SendAsync(new GetProjectsPhrWithWeeklyStatusesQuery{YearWeek = yearWeek});

            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProject([FromRoute]int id)
        {
            var dto = await _mediator.SendAsync(new GetProjectQuery(){ProjectId = id});

            return Ok(dto);
        }

        [HttpGet("phr/project-with-yearweeks-statuses/{projectId}")]
        public async Task<ActionResult> GetProjectPhrWithReportYearWeeksAndStatuses(int projectId)
        {
            var dto = await _mediator.SendAsync(new GetProjectPhrWithReportYearWeeksAndStatusesQuery{ProjectId = projectId});

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

        [HttpPut("non-master-data")]
        public async Task<ActionResult> EditProjectNonMasterData([FromBody] EditProjectNonMasterDataCommand command)
        {
            var dto = await _mediator.SendAsync(command);

            return Ok(dto);
        }

        [HttpGet("caching/{id}")]
        public async Task<ActionResult> GetProjectCaching(int id)
        {
            var dto = await _mediator.SendAsync(new GetProjectCachingQuery(){ProjectId = id});

            return Ok(dto);
        }
        
        
    }
}