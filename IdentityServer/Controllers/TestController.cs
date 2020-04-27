using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Features;
using IdentityServer.Features.Business.Clients;
using IdentityServer.Features.Business.ScopeProviders;
using IdentityServer.Features.Business.Users;
using IdentityServer.Features.Domains;
using Microsoft.AspNetCore.Mvc;
using ResponsibilityChain.Business;

namespace IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IdPDbContext _dbContext;

        public TestController(IMediator mediator, IdPDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }

        [HttpGet("clients")]
        public async Task<ActionResult> GetClients()
        {
            var dto = await _mediator.SendAsync(new GetClients());
            return Ok(dto);
        }
        
        [HttpGet("scopes")]
        public async Task<ActionResult> GetScopes()
        {
            var dto = await _mediator.SendAsync(new GetScopes());
            return Ok(dto);
        }
        
        [HttpGet("users")]
        public async Task<ActionResult> GetUsers()
        {
            var dto = await _mediator.SendAsync(new GetUsers());

            return Ok(dto);
        }

        [HttpPost]
        public IActionResult SeedData()
        {
            var resources = new List<string>
            {
                // ApiResources.Project,
                ApiResources.ProjectAccess,
                // ApiResources.ProjectNonMaster,
            };
            
            _dbContext.Scopes.Where(s => (s.Action == Actions.Create) && resources.Contains(s.Resource)).Select(s => s.Id).ToList()
                .ForEach(s => _dbContext.PolicyScopes.Add(new PolicyScope("EditProjectNonMaster", s)));

            _dbContext.SaveChanges();

            return Ok();
        }
    }
}