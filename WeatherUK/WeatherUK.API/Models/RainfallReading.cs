namespace WeatherUK.API.Models
{
    /// <summary>
    /// Details of a rainfall reading
    /// </summary>
    public class RainfallReading
    {
        public string DateMeasured { get; set; }
        public Decimal AmountMeasured { get; set; }
    }
}
