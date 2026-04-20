using Newtonsoft.Json;
using Nexora.WebUI.DTOs.CryptoPriceDtos;

namespace Nexora.WebUI.Services.CryptoPriceServices
{
    public class CryptoPriceService : ICryptoPriceService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CryptoPriceService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResultCryptoPriceDto> CryptoPrices()
        {
            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://crypto-price-api.p.rapidapi.com/cryptoPrice?symbol=Ethereum%2CBitcoin"),
                Headers =
    {
        { "x-rapidapi-key", "your-api-key" },
        { "x-rapidapi-host", "crypto-price-api.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultCryptoPriceDto>(jsonData);
                return values;
            }
        }
    }
}
