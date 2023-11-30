using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace WeatherUK.Infrastructure
{
    public class RainfallClient : IRainfallClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<RainfallClient> _logger;

        public RainfallClient(IHttpClientFactory httpClientFactory, ILogger<RainfallClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<RainfallReading[]> GetReadingsAsync(string stationId)
        {
            using HttpClient client = _httpClientFactory.CreateClient();

            try
            {
                var readings = await client.GetFromJsonAsync<RainfallReading[]>(
                    $"https://environment.data.gov.uk/flood-monitoring/id/stations/{stationId}/readings",
                    new JsonSerializerOptions(JsonSerializerDefaults.Web));

                return readings ?? Array.Empty<RainfallReading>();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting something fun to say: {Error}", ex);
            }

            return Array.Empty<RainfallReading>();
        }
    }
}