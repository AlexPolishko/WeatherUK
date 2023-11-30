using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

        /// <summary>
        /// Get rainfall readings by station Id.
        /// </summary>
        /// <param name="stationId">The id of the reading station</param>
        /// <param name="count">The number of readings to return</param>
        [HttpGet]
        [Route("id/{stationId}/readings")]
        [SwaggerResponse(200, "A list of rainfall readings successfully retrieved", typeof(WeatherForecast[]))]
        [SwaggerResponse(400, "Invalid request")]
        [SwaggerResponse(404, "No readings found for the specified stationId")]
        [SwaggerResponse(500, "Internal server error")]
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