namespace WeatherUK.API.Models
{
    /// <summary>
    /// Details of invalid request property
    /// </summary>
    public class ErrorDetail
    {
        public string PropertyName { get; set; }
        public string Message { get; set; }
    }
}
