using Nexora.WebUI.DTOs.HotelDtos;

namespace Nexora.WebUI.Services.HotelServices
{
    public interface IHotelSearchService
    {
        Task<List<HotelCardVm>> SearchHotels(
            string destId,
            string searchType,
            DateTime arrivalDate,
            DateTime departureDate,
            int adults,
            int childrenCount,
            int roomQty);
    }
}
