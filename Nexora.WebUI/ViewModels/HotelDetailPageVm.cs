namespace Nexora.WebUI.ViewModels
{
    public class HotelDetailPageVm
    {
        public int HotelId { get; set; }
        public string Name { get; set; } = "";
        public int ReviewCount { get; set; }
        public string ArrivalDate { get; set; } = "";
        public string DepartureDate { get; set; } = "";
        public string CheckinTime { get; set; } = "";
        public string CheckoutTime { get; set; } = "";
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; } = "";
        public string City { get; set; } = "";
        public string Country { get; set; } = "";
        public decimal PricePerNight { get; set; }
        public string PriceCurrency { get; set; } = "";
        public List<string> GalleryUrls { get; set; } = new();
        public List<string> Highlights { get; set; } = new();
        public List<string> FamilyFacilities { get; set; } = new();
        public List<string> PopularFacilities { get; set; } = new();
        public List<string> AreaBenefits { get; set; } = new();
        public string RoomDescription { get; set; } = "";
        public string BookingUrl { get; set; } = "";

        public string SearchCity { get; set; } = "";
        public string DestinationLabel { get; set; } = "";
        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public int Rooms { get; set; }
    }
}
