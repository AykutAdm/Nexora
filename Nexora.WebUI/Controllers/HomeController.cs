using Microsoft.AspNetCore.Mvc;
using Nexora.WebUI.Services.DestinationServices;
using Nexora.WebUI.Services.HotelServices;
using Nexora.WebUI.ViewModels;

namespace Nexora.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDestinationService _destinationService;
        private readonly IHotelSearchService _hotelSearchService;
        private readonly IHotelDetailService _hotelDetailService;

        public HomeController(
            IDestinationService destinationService,
            IHotelSearchService hotelSearchService,
            IHotelDetailService hotelDetailService)
        {
            _destinationService = destinationService;
            _hotelSearchService = hotelSearchService;
            _hotelDetailService = hotelDetailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Results(
            string city,
            DateTime checkin,
            DateTime checkout,
            int adults,
            int children,
            int rooms)
        {
            var dest = await _destinationService.GetCityIdByName(city);
            var first = dest.data[0];
            var destId = first.dest_id;
            var searchType = first.search_type.ToUpperInvariant();

            var hotels = await _hotelSearchService.SearchHotels(
                destId,
                searchType,
                checkin,
                checkout,
                adults,
                children,
                rooms);

            var vm = new HotelSearchPageVm
            {
                City = city,
                DestinationLabel = first.label,
                Checkin = checkin,
                Checkout = checkout,
                Adults = adults,
                Children = children,
                Rooms = rooms,
                Hotels = hotels,
            };

            return View(vm);
        }

        public async Task<IActionResult> Detail(
            int hotelId,
            DateTime checkin,
            DateTime checkout,
            int adults,
            int children,
            int rooms,
            string city,
            string destinationLabel)
        {
            var vm = await _hotelDetailService.GetHotelDetail(
                hotelId,
                checkin,
                checkout,
                adults,
                children,
                rooms,
                city,
                destinationLabel);

            return View(vm);
        }
    }
}
