using Nexora.WebUI.DTOs.CryptoPriceDtos;

namespace Nexora.WebUI.Services.CryptoPriceServices
{
    public interface ICryptoPriceService
    {
        Task<ResultCryptoPriceDto> CryptoPrices();
    }
}
