using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dependency_Injection.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Dependency_Injection.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IEnumerable<IRule> _rules;
        private readonly IWeatherFocaster _weatherFocaster;
        public WeatherForecastController(
            IWeatherFocaster weatherFocaster, 
            ILogger<WeatherForecastController> logger,
            // DI Container will resolve all instances of IRule and inject them as an IEnumerable.
            IEnumerable<IRule> rules
            )
        {
            _weatherFocaster = weatherFocaster;
            _logger = logger;
            _rules = rules;
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

       

        [HttpGet]
        // Action Injection: injecting the services into an Action Method. The services is resolved only when this endpoint is called
        // and not when this controller class is created (as it will be creating all the dependecies injected through its Ctor).
        public IActionResult Get([FromServices]IWeatherFocaster weather)
        {
            var season = weather.GetSeason();

            return Ok(season);
        }

        [HttpGet("rules")]
        public IActionResult GetRules()
        {
            var res = new List<string>();

            foreach (var rule in _rules)
            {
                res.Add(rule.PassedRule());    
            }

            return Ok(res);
        }
    }
}
