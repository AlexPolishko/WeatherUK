using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WeatherUK.API.Models;
using WeatherUK.Infrastructure;

namespace WeatherUK.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RainfallController : ControllerBase
    {

        private readonly ILogger<RainfallController> _logger;
        private IRainfallClient _rainfallClient;

        public RainfallController(ILogger<RainfallController> logger, IRainfallClient rainfallClient)
        {
            _logger = logger;
            _rainfallClient = rainfallClient;
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
        public async Task<IActionResult> Get(string stationId, [FromQuery] int count = 10)
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

            var result = await _rainfallClient.GetReadingsAsync(stationId, count);

            return Ok( new RainfallReadingResponse
            {
                Readings = result.Select(readings => new Models.RainfallReading
                {
                    DateMeasured = readings.DateTime.ToShortDateString(),
                    AmountMeasured = readings.Value,
                })
            .ToArray()
            });
        }
    }
}