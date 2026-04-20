using Nexora.WebUI.DTOs.ExchangeRateDtos;

namespace Nexora.WebUI.Services.ExchangeRateService
{
    public interface IExchangeRateService
    {
        Task<ResultExchangeRateDto> ExchangeRate();
    }
}
