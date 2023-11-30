using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
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

        public async Task<RainfallReading[]> GetReadingsAsync(string stationId, int limit)
        {
            using HttpClient client = _httpClientFactory.CreateClient();

            try
            {
                var response = await client.GetAsync($"https://environment.data.gov.uk/flood-monitoring/id/stations/{stationId}/readings?_limit={limit}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var jsonObject = JObject.Parse(json);

                    var fieldValue = jsonObject["items"];

                    if (fieldValue is JArray array)
                    {
                        var result = array.Select(
                                        x => new RainfallReading
                                        {
                                            DateTime = DateTime.Parse(x["dateTime"].ToString()),
                                            Value = decimal.Parse(x["value"].ToString())
                                        }).ToArray();

                        return result;
                    }
                }

                return Array.Empty<RainfallReading>();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {Error}", ex);
                throw;
            }

            return Array.Empty<RainfallReading>();
        }
    }
}