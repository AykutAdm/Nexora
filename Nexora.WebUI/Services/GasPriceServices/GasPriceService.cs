using Newtonsoft.Json;
using Nexora.WebUI.DTOs.GasPriceDtos;

namespace Nexora.WebUI.Services.GasPriceServices
{
    public class GasPriceService : IGasPriceService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GasPriceService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResultGasPriceDto> GasPrices()
        {
            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://gas-price.p.rapidapi.com/europeanCountries"),
                Headers =
    {
        { "x-rapidapi-key", "your-api-key" },
        { "x-rapidapi-host", "gas-price.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultGasPriceDto>(jsonData);
                return values;
            }
        }
    }
}
