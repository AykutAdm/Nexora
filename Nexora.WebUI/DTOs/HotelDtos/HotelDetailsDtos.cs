using Newtonsoft.Json;

namespace Nexora.WebUI.DTOs.HotelDtos
{
    public class HotelDetailsApiResponseDto
    {
        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("data")]
        public HotelDetailsDataDto Data { get; set; } = new();
    }

    public class HotelDetailsDataDto
    {
        [JsonProperty("hotel_id")]
        public int HotelId { get; set; }

        [JsonProperty("hotel_name")]
        public string HotelName { get; set; } = "";

        [JsonProperty("url")]
        public string Url { get; set; } = "";

        [JsonProperty("review_nr")]
        public int ReviewNr { get; set; }

        [JsonProperty("arrival_date")]
        public string ArrivalDate { get; set; } = "";

        [JsonProperty("departure_date")]
        public string DepartureDate { get; set; } = "";

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; } = "";

        [JsonProperty("city")]
        public string City { get; set; } = "";

        [JsonProperty("country_trans")]
        public string CountryTrans { get; set; } = "";

        [JsonProperty("family_facilities")]
        public List<string> FamilyFacilities { get; set; } = new();

        [JsonProperty("property_highlight_strip")]
        public List<PropertyHighlightItemDto> PropertyHighlightStrip { get; set; } = new();

        [JsonProperty("facilities_block")]
        public FacilitiesBlockDataDto FacilitiesBlock { get; set; } = new();

        [JsonProperty("top_ufi_benefits")]
        public List<TopUfiBenefitItemDto> TopUfiBenefits { get; set; } = new();

        [JsonProperty("rooms")]
        public Dictionary<string, HotelRoomDetailDto> Rooms { get; set; } = new();

        [JsonProperty("composite_price_breakdown")]
        public PriceBreakdownBlockDto CompositePriceBreakdown { get; set; } = new();

        [JsonProperty("checkin")]
        public HotelTimeBlockDto Checkin { get; set; } = new();

        [JsonProperty("checkout")]
        public HotelTimeBlockDto Checkout { get; set; } = new();
    }

    public class TopUfiBenefitItemDto
    {
        [JsonProperty("translated_name")]
        public string TranslatedName { get; set; } = "";
    }

    public class PropertyHighlightItemDto
    {
        [JsonProperty("name")]
        public string Name { get; set; } = "";
    }

    public class FacilitiesBlockDataDto
    {
        [JsonProperty("facilities")]
        public List<FacilityNameDto> Facilities { get; set; } = new();
    }

    public class FacilityNameDto
    {
        [JsonProperty("name")]
        public string Name { get; set; } = "";
    }

    public class HotelRoomDetailDto
    {
        [JsonProperty("description")]
        public string Description { get; set; } = "";

        [JsonProperty("photos")]
        public List<HotelRoomPhotoDto> Photos { get; set; } = new();
    }

    public class HotelRoomPhotoDto
    {
        [JsonProperty("url_max1280")]
        public string UrlMax1280 { get; set; } = "";

        [JsonProperty("url_max750")]
        public string UrlMax750 { get; set; } = "";
    }

    public class PriceBreakdownBlockDto
    {
        [JsonProperty("gross_amount_per_night")]
        public MoneyValueDto GrossAmountPerNight { get; set; } = new();
    }

    public class MoneyValueDto
    {
        [JsonProperty("value")]
        public decimal Value { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; } = "";
    }

    public class HotelTimeBlockDto
    {
        [JsonProperty("from_time")]
        public string FromTime { get; set; } = "";

        [JsonProperty("until_time")]
        public string UntilTime { get; set; } = "";

        [JsonProperty("fromTime")]
        public string FromTimeCamel { get; set; } = "";

        [JsonProperty("untilTime")]
        public string UntilTimeCamel { get; set; } = "";
    }
}
