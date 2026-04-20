using Nexora.WebUI.DTOs.HotelDtos;

namespace Nexora.WebUI.ViewModels
{
    public class HotelSearchPageVm
    {
        public string City { get; set; } = "";
        public string DestinationLabel { get; set; } = "";
        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public int Rooms { get; set; }
        public List<HotelCardVm> Hotels { get; set; } = new();
    }
}
