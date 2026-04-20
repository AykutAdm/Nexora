using Nexora.WebUI.DTOs.WeatherForecastDtos;

namespace Nexora.WebUI.Services.WeatherForecastServices
{
    public interface IWeatherForecastService
    {
        Task<ResultWeatherForecastDto> WeatherForecast();
    }
}
