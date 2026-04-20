using Nexora.WebUI.ViewModels;

namespace Nexora.WebUI.Services.HotelServices
{
    public interface IHotelDetailService
    {
        Task<HotelDetailPageVm> GetHotelDetail(
            int hotelId,
            DateTime checkin,
            DateTime checkout,
            int adults,
            int children,
            int rooms,
            string searchCity,
            string destinationLabel);
    }
}
