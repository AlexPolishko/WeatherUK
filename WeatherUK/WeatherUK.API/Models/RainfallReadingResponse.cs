namespace WeatherUK.API.Models
{
    /// <summary>
    ///  Details of a rainfall reading
    /// </summary>
    public class RainfallReadingResponse
    {
        public RainfallReading[] Readings { get; set; }
    }
}
