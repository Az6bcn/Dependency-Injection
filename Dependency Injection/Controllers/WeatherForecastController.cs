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
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
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
