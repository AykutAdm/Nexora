using Nexora.WebUI.DTOs.GasPriceDtos;

namespace Nexora.WebUI.Services.GasPriceServices
{
    public interface IGasPriceService
    {
        Task<ResultGasPriceDto> GasPrices();
    }
}
