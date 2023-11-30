namespace WeatherUK.API.Models
{
    /// <summary>
    ///  Details of a rainfall reading
    /// </summary>
    public class ErrorResponse
    {
        public string Message { get; set; }
        public ErrorDetail[] Detail { get; set; }
    }
}
