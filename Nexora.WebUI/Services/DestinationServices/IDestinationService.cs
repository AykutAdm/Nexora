using Nexora.WebUI.DTOs.DestinationDtos;

namespace Nexora.WebUI.Services.DestinationServices
{
    public interface IDestinationService
    {
        Task<GetDestinationByIdDto> GetCityIdByName(string query);
    }
}
