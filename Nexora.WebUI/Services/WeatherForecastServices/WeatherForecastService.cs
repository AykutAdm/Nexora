using Newtonsoft.Json;
using Nexora.WebUI.DTOs.WeatherForecastDtos;

namespace Nexora.WebUI.Services.WeatherForecastServices
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherForecastService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResultWeatherForecastDto> WeatherForecast()
        {
            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://open-weather13.p.rapidapi.com/city?city=%C4%B0stanbul&lang=TR"),
                Headers =
                {
                    { "x-rapidapi-key", "your-api-key" },
                    { "x-rapidapi-host", "open-weather13.p.rapidapi.com" },
                },
            };

            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<ResultWeatherForecastDto>(jsonData);
            NormalizeOpenWeather13ToSi(values);
            return values;
        }

        // open-weather13 /city returns °F and mph (units=metric is ignored); UI expects °C and km/h.
        private static void NormalizeOpenWeather13ToSi(ResultWeatherForecastDto? dto)
        {
            if (dto?.main == null)
                return;

            dto.main.temp = FahrenheitToCelsius(dto.main.temp);
            dto.main.feels_like = FahrenheitToCelsius(dto.main.feels_like);

            if (dto.wind != null)
                dto.wind.speed *= 1.609344f; // mph → km/h (OpenWeatherMap imperial)
        }

        private static float FahrenheitToCelsius(float fahrenheit) => (fahrenheit - 32f) * (5f / 9f);
    }
}
