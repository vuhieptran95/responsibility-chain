using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ResponsibilityChain.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IThing<WeatherForecastController, WeatherForecast> _thing;
        private readonly Handler<Request1, Response1> _handler;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger, 
            IThing<WeatherForecastController, WeatherForecast> thing,
            Handler<Request1, Response1> handler)
        {
            _logger = logger;
            _thing = thing;
            _handler = handler;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            var thing = _thing.GetName;

            var response1 = await _handler.HandleAsync(new Request1());
            
            return response1.Type;
        }
    }
}