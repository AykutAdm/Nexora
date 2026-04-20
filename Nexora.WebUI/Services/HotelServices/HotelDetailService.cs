using System.Linq;
using Newtonsoft.Json;
using Nexora.WebUI.DTOs.HotelDtos;
using Nexora.WebUI.ViewModels;

namespace Nexora.WebUI.Services.HotelServices
{
    public class HotelDetailService : IHotelDetailService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HotelDetailService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HotelDetailPageVm> GetHotelDetail(
            int hotelId,
            DateTime checkin,
            DateTime checkout,
            int adults,
            int children,
            int rooms,
            string searchCity,
            string destinationLabel)
        {
            var childrenAge = children > 0
                ? string.Join(",", Enumerable.Repeat(10, children))
                : "";

            var query = new List<string>
            {
                $"hotel_id={hotelId}",
                $"arrival_date={checkin:yyyy-MM-dd}",
                $"departure_date={checkout:yyyy-MM-dd}",
                $"adults={adults}",
                $"room_qty={rooms}",
                "units=metric",
                "temperature_unit=c",
                "languagecode=en-us",
                "currency_code=USD",
                "location=US",
            };

            if (children > 0)
                query.Add($"children_age={Uri.EscapeDataString(childrenAge)}");

            var url = "https://booking-com15.p.rapidapi.com/api/v1/hotels/getHotelDetails?" + string.Join("&", query);

            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
                Headers =
                {
                    { "x-rapidapi-key", "your-api-key" },
                    { "x-rapidapi-host", "booking-com15.p.rapidapi.com" },
                },
            };

            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var jsonData = await response.Content.ReadAsStringAsync();
            var root = JsonConvert.DeserializeObject<HotelDetailsApiResponseDto>(jsonData)!;
            var d = root.Data;

            var gallery = new List<string>();
            var roomDescription = "";
            foreach (var room in d.Rooms.Values)
            {
                if (roomDescription == "")
                    roomDescription = room.Description;
                foreach (var photo in room.Photos)
                    gallery.Add(photo.UrlMax1280 != "" ? photo.UrlMax1280 : photo.UrlMax750);
            }

            var highlights = d.PropertyHighlightStrip.Select(h => h.Name).ToList();
            var popular = d.FacilitiesBlock.Facilities.Select(f => f.Name).ToList();
            var benefits = d.TopUfiBenefits.Select(b => b.TranslatedName).ToList();
            var night = d.CompositePriceBreakdown.GrossAmountPerNight;
            var bookingUrl = (d.Url ?? "").Trim();
            if (bookingUrl.Length > 0 && !bookingUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                bookingUrl = "https://www.booking.com" + (bookingUrl.StartsWith("/") ? "" : "/") + bookingUrl;

            return new HotelDetailPageVm
            {
                HotelId = d.HotelId,
                Name = d.HotelName,
                ReviewCount = d.ReviewNr,
                BookingUrl = bookingUrl,
                ArrivalDate = d.ArrivalDate != "" ? d.ArrivalDate : checkin.ToString("yyyy-MM-dd"),
                DepartureDate = d.DepartureDate != "" ? d.DepartureDate : checkout.ToString("yyyy-MM-dd"),
                CheckinTime = d.Checkin.FromTime != "" ? d.Checkin.FromTime : d.Checkin.FromTimeCamel,
                CheckoutTime = d.Checkout.UntilTime != "" ? d.Checkout.UntilTime : d.Checkout.UntilTimeCamel,
                Latitude = d.Latitude,
                Longitude = d.Longitude,
                Address = d.Address,
                City = d.City,
                Country = d.CountryTrans,
                PricePerNight = night.Value,
                PriceCurrency = night.Currency,
                GalleryUrls = gallery,
                Highlights = highlights,
                FamilyFacilities = d.FamilyFacilities,
                PopularFacilities = popular,
                AreaBenefits = benefits,
                RoomDescription = roomDescription,
                SearchCity = searchCity,
                DestinationLabel = destinationLabel,
                Checkin = checkin,
                Checkout = checkout,
                Adults = adults,
                Children = children,
                Rooms = rooms,
            };
        }
    }
}
