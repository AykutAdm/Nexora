using Newtonsoft.Json;
using Nexora.WebUI.DTOs.HotelDtos;

namespace Nexora.WebUI.Services.HotelServices
{
    public class HotelSearchService : IHotelSearchService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HotelSearchService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<HotelCardVm>> SearchHotels(
            string destId,
            string searchType,
            DateTime arrivalDate,
            DateTime departureDate,
            int adults,
            int childrenCount,
            int roomQty)
        {
            var childrenAge = childrenCount > 0
                ? string.Join(",", Enumerable.Repeat(10, childrenCount))
                : "";

            var query = new List<string>
            {
                $"dest_id={destId}",
                $"search_type={searchType}",
                $"arrival_date={arrivalDate:yyyy-MM-dd}",
                $"departure_date={departureDate:yyyy-MM-dd}",
                $"adults={adults}",
                $"room_qty={roomQty}",
                "page_number=1",
                "units=metric",
                "temperature_unit=c",
                "languagecode=en-us",
                "currency_code=USD",
                "location=US",
            };

            if (childrenCount > 0)
                query.Add($"children_age={Uri.EscapeDataString(childrenAge)}");

            var url = "https://booking-com15.p.rapidapi.com/api/v1/hotels/searchHotels?" + string.Join("&", query);

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

            var values = JsonConvert.DeserializeObject<HotelSearchResponseDto>(jsonData);
            var nights = Math.Max(1, (departureDate - arrivalDate).Days);
            var cards = new List<HotelCardVm>();

            foreach (var item in values.data.hotels)
            {
                var p = item.property;
                var pb = p.priceBreakdown.grossPrice;
                var gross = pb.value > 0 ? pb.value : pb.hotelAmount.amount;
                var nightPrice = Math.Round(gross / nights, 2);

                var rawImg = p.photoUrls.Count > 0 ? p.photoUrls[0] : "";
                var img = rawImg.Replace("square60", "max500", StringComparison.OrdinalIgnoreCase);

                var stars = p.propertyClass > 0 ? p.propertyClass : p.accuratePropertyClass;

                cards.Add(new HotelCardVm
                {
                    Id = p.id,
                    Name = p.name,
                    ImageUrl = img,
                    NightPrice = nightPrice,
                    Stars = stars,
                });
            }

            return cards;
        }
    }
}
