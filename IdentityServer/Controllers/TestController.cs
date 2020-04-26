using System.Diagnostics;
using System.Threading.Tasks;
using IdentityServer.Features.Business.Clients;
using IdentityServer.Features.Business.ScopeProviders;
using IdentityServer.Features.Business.Users;
using Microsoft.AspNetCore.Mvc;
using ResponsibilityChain.Business;

namespace IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        private readonly IMediator _mediator;

        public TestController(IMediator mediator)
        {
            _mediator = mediator;
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
    }
}