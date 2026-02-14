using Microsoft.AspNetCore.Mvc;

namespace EventsMS.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
