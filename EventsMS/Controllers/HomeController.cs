using System.Diagnostics;
using EventsMS.Models;
using EventsMS.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EventsMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEventRepository _eventRepository;

       
        public HomeController(ILogger<HomeController> logger, IEventRepository eventRepository)
        {
            _logger = logger;
            _eventRepository = eventRepository;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _eventRepository.GetAllEventAsync(CancellationToken.None);
            return View(data);
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
    }
}
