using System.Diagnostics;
using EventsMS.Helper;
using EventsMS.Models;
using EventsMS.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EventsMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEventRepository _eventRepository;
        private readonly ISignInHelper _signInHelper;

        public HomeController(ILogger<HomeController> logger, IEventRepository eventRepository, ISignInHelper signInHelper)
        {
            _logger = logger;
            _eventRepository = eventRepository;
            _signInHelper = signInHelper;
        }

        public async Task<IActionResult> Index()
        
        {
            /// Fetch all events from the repository
            var allEvents = await _eventRepository.GetAllEventAsync(CancellationToken.None);

            // Filter events to include only those that are ongoing (current date is between start and end date)
            var ongoingEvents = allEvents
                                .Where(e => e.EndDate >= DateTime.UtcNow)
                                .ToList();

            return View(ongoingEvents);
        }

        [HttpGet]
        public async Task<IActionResult> Details(long id, CancellationToken cancellationToken)
        {
            var data = await _eventRepository.GeEventByIdAsync(id, cancellationToken);
            if (data != null)
            {
                return View(data);
            }
            return NotFound();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Contact()
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
