using Microsoft.AspNetCore.Mvc;

namespace WeatherUK.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RainfallController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<RainfallController> _logger;

        public RainfallController(ILogger<RainfallController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("id/{stationId}/readings")]
        public IActionResult Get(string stationId, [FromQuery] int count = 10)
        {
            if (count < 1 || count > 100) return BadRequest();

            return Ok(Enumerable.Range(1, count).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray());
        }
    }
}