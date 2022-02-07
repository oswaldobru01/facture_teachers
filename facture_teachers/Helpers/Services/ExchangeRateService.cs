using facture_teachers.Models.Response;
using RestSharp;
using System.Text.Json;

namespace facture_teachers.Helpers.Services
{
    public class ExchangeRateService
    {
        private readonly IConfiguration _configuration;
        public ExchangeRateService(IConfiguration _config)
        {
            _configuration = _config;
        }

        public async Task<ExchangeRateResponse> FindExchangeByCode(string _code) {

            var client = new RestClient(GetUrlExchagneRateAPI(_code));
            var request = new RestRequest(String.Empty, Method.Get);
            var response = await client.ExecuteGetAsync(request);
            var jsonResponse = JsonSerializer.Deserialize<ExchangeRateResponse>(response.Content);
            return jsonResponse;
        }

        private string GetUrlExchagneRateAPI(string _code)
        {
            var API = _configuration.GetSection("AppSettings").GetSection("UrlExchangeAPI").Value;
            var token = _configuration.GetSection("AppSettings").GetSection("tokenExchangeAPI").Value;
            return string.Format(API, token, _code);
        }
    }
}
