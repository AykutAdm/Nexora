using Microsoft.AspNetCore.Mvc;
using Nexora.WebUI.DTOs.CryptoPriceDtos;
using Nexora.WebUI.Services.CryptoPriceServices;

namespace Nexora.WebUI.ViewComponents
{
    public class _HomeCryptoPriceComponentPartial : ViewComponent
    {
        private readonly ICryptoPriceService _cryptoPriceService;

        public _HomeCryptoPriceComponentPartial(ICryptoPriceService cryptoPriceService)
        {
            _cryptoPriceService = cryptoPriceService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _cryptoPriceService.CryptoPrices();
            return View(values);
        }
    }
}
