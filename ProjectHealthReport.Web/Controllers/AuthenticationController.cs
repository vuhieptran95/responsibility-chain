using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjectHealthReport.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : Controller
    {
        [HttpGet]
        public IActionResult Get([FromQuery]string redirectUrlIdP)
        {
            return Ok();
        }
    }
}