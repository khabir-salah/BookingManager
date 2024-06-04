using BookingManager.Models;
using BookingManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookingManager.Controllers
{
    public class HomeController : Controller
    {
        readonly IApartmentService _apartmentService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IApartmentService apartmentService)
        {
            _apartmentService = apartmentService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SearchResult(SearchDTO request)
        {
            var apartments = _apartmentService.SearchApartment(request);
            return View("SearchResult", apartments);
        }

        public IActionResult BookApartment(int id)
        {
            var book = _apartmentService.BookApartment(id);
            return View("BookApartment", book);
        }
    }
}
