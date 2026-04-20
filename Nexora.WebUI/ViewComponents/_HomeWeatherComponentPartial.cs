using Microsoft.AspNetCore.Mvc;
using Nexora.WebUI.DTOs.WeatherForecastDtos;
using Nexora.WebUI.Services.WeatherForecastServices;

namespace Nexora.WebUI.ViewComponents
{
    public class _HomeWeatherComponentPartial : ViewComponent
    {
        private readonly IWeatherForecastService _weatherForecastService;

        public _HomeWeatherComponentPartial(IWeatherForecastService weatherForecastService)
        {
            _weatherForecastService = weatherForecastService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _weatherForecastService.WeatherForecast();
            return View(values);
        }
    }
}
