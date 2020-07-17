using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackATL_Server.Models.Repository.Services_MongoDB;
using HackATL_Server.Repository.Interfaces_MongoDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HackATL_Server.Controllers
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
        IUserService_md _user; 

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUserService_md userService)
        {
            _user = userService;
            _logger = logger;
        }

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

        [HttpGet("test")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult NameCheck(string hi)
        {
            var x = _user.GetUser("Gabe");
            if (x != null)
                return Ok(x);
            else
            {
                return BadRequest();
            }
        }
    }
}
