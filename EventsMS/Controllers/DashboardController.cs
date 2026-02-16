using Microsoft.AspNetCore.Mvc;

namespace EventsMS.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
