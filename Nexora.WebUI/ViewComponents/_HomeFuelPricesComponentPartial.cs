using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nexora.WebUI.Services.GasPriceServices;

namespace Nexora.WebUI.ViewComponents
{
    public class _HomeFuelPricesComponentPartial : ViewComponent
    {
        private readonly IGasPriceService _gasPriceService;

        public _HomeFuelPricesComponentPartial(IGasPriceService gasPriceService)
        {
            _gasPriceService = gasPriceService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _gasPriceService.GasPrices();
            var row = values.result.FirstOrDefault(r =>
                string.Equals(r?.country, "Turkey", StringComparison.OrdinalIgnoreCase));

            return View(row);
        }
    }
}
