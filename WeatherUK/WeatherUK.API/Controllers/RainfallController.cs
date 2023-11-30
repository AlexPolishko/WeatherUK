using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WeatherUK.API.Models;

namespace WeatherUK.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RainfallController : ControllerBase
    {

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
        /// <returns>Retrieve the latest readings for the specified station</returns>
        [HttpGet]
        [Route("id/{stationId}/readings")]
        [SwaggerResponse(200, "A list of rainfall readings successfully retrieved", typeof(RainfallReadingResponse[]))]
        [SwaggerResponse(400, "Invalid request", typeof(ErrorResponse))]
        [SwaggerResponse(404, "No readings found for the specified stationId",typeof(ErrorResponse))]
        [SwaggerResponse(500, "Internal server error")]
        public IActionResult Get(string stationId, [FromQuery] int count = 10)
        {
            if (count < 1 || count > 100)
            {
                return BadRequest(new ErrorResponse {
                    Message = "Number should be in range (1,100)", 
                    Detail = new[] {
                        new ErrorDetail{
                            Message = "Number should be in range (1,100)", 
                            PropertyName = nameof(count) 
                        } 
                    } 
                });
            }

            return Ok( new RainfallReadingResponse
            {
                Readings =
                Enumerable.Range(1, count).Select(index => new RainfallReading
                {
                    DateMeasured = DateTime.Now.AddDays(index).ToShortDateString(),
                    AmountMeasured = Random.Shared.Next(-20, 55),
                })
            .ToArray()
            });
        }
    }
}