namespace Nexora.WebUI.DTOs.HotelDtos
{
    public class HotelSearchResponseDto
    {
        public bool status { get; set; }
        public HotelSearchDataDto data { get; set; }
    }

    public class HotelSearchDataDto
    {
        public List<HotelSearchItemDto> hotels { get; set; }
    }

    public class HotelSearchItemDto
    {
        public HotelPropertyDto property { get; set; }
    }

    public class HotelPropertyDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<string> photoUrls { get; set; }
        public int propertyClass { get; set; }
        public int accuratePropertyClass { get; set; }
        public HotelPriceBreakdownDto priceBreakdown { get; set; }
    }

    public class HotelPriceBreakdownDto
    {
        public HotelGrossPriceDto grossPrice { get; set; }
    }

    public class HotelGrossPriceDto
    {
        public decimal value { get; set; }
        public HotelMoneyAmountDto hotelAmount { get; set; }
    }

    public class HotelMoneyAmountDto
    {
        public decimal amount { get; set; }
    }

    public class HotelCardVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal NightPrice { get; set; }
        public int Stars { get; set; }
    }
}
