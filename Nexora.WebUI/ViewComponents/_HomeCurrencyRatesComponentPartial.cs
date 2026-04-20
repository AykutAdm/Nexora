using Microsoft.AspNetCore.Mvc;
using Nexora.WebUI.Services.ExchangeRateService;
using System.Threading.Tasks;

namespace Nexora.WebUI.ViewComponents
{
    public class _HomeCurrencyRatesComponentPartial : ViewComponent
    {
        private readonly IExchangeRateService _exchangeRateService;

        public _HomeCurrencyRatesComponentPartial(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _exchangeRateService.ExchangeRate();
            ViewBag.USD = values.rates.USD;
            ViewBag.EUR = values.rates.EUR;
            ViewBag.RUB = values.rates.RUB;
            return View();
        }
    }
}
