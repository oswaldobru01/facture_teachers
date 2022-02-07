namespace facture_teachers.Models.Response
{
    public class ExchangeRateResponse
    {
        public string result { get; set; } = string.Empty;
        public string base_code { get; set; } = string.Empty;
        public ConversionRate? conversion_rates { get; set; } = null;
    }

    public class ConversionRate
    {
        public decimal COP { get; set; }
    }
}