using Newtonsoft.Json;
using Nexora.WebUI.DTOs.DestinationDtos;

namespace Nexora.WebUI.Services.DestinationServices
{
    public class DestinationService : IDestinationService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DestinationService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<GetDestinationByIdDto> GetCityIdByName(string query)
        {
            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://booking-com15.p.rapidapi.com/api/v1/hotels/searchDestination?query={query}"),
                Headers =
                {
                    { "x-rapidapi-key", "your-api-key" },
                    { "x-rapidapi-host", "booking-com15.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetDestinationByIdDto>(jsonData);
                return values;
            }
        }
    }
}
