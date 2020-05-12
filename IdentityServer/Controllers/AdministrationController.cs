using System.Threading.Tasks;
using IdentityServer.Features.Business.Scopes;
using IdentityServer.Features.Business.Users;
using Microsoft.AspNetCore.Mvc;
using ResponsibilityChain.Business;

namespace IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationController : Controller
    {
        private readonly IMediator _mediator;

        public AdministrationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("users")]
        public async Task<ActionResult> GetUsersWithScopes()
        {
            var dto = await _mediator.SendAsync(new GetUsersWithScopes());

            return Ok(dto); 
        }
        
        [HttpGet("policies/{policyId}/scopes")]
        public async Task<ActionResult> GetScopesByPolicyId(string policyId)
        {
            var dto = await _mediator.SendAsync(new GetScopesByPolicyId(){PolicyId = policyId});

            return Ok(dto); 
        }
    }
}