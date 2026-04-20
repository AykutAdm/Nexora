using Newtonsoft.Json;
using Nexora.WebUI.DTOs.ExchangeRateDtos;

namespace Nexora.WebUI.Services.ExchangeRateService
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ExchangeRateService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResultExchangeRateDto> ExchangeRate()
        {
            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://currency-conversion-and-exchange-rates.p.rapidapi.com/latest?base=TRY&symbols=EUR%2CUSD%2CRUB"),
                Headers =
    {
        { "x-rapidapi-key", "your-api-key" },
        { "x-rapidapi-host", "currency-conversion-and-exchange-rates.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultExchangeRateDto>(jsonData);
                return values;
            }
        }
    }
}
