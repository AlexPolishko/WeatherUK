namespace WeatherUK.Infrastructure
{
    public interface IRainfallClient
    {
        Task<RainfallReading[]> GetReadingsAsync(string stationId, int limit);
    }
}