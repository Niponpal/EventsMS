using EventsMS.Models;
using EventsMS.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EventsMS.Controllers
{
    public class PaymenthistoryController : Controller
    {
        private readonly IPaymentHistoryRepository _paymentHistoryRepository;
        public PaymenthistoryController(IPaymentHistoryRepository paymentHistoryRepository)
        {
            _paymentHistoryRepository = paymentHistoryRepository;
        }
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var data = await _paymentHistoryRepository.GetAllPaymentHistoryAsync(cancellationToken);
            if (data != null)
            {
                return View(data);
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> CreateOrEdit(long id, CancellationToken cancellationToken)
        {
            if (id == 0)
            {
                return View(new PaymentHistory());
            }
            else
            {
                var data = await _paymentHistoryRepository.GetPaymentHistoryByIdAsync(id, cancellationToken);
                if (data != null)
                {
                    return View(data);
                }
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrEdit(PaymentHistory paymentHistory, CancellationToken cancellationToken)
        {

            if (paymentHistory.Id == 0)
            {
                await _paymentHistoryRepository.AddPaymentHistoryAsync(paymentHistory, cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                await _paymentHistoryRepository.UpdatePaymentHistoryAsync(paymentHistory, cancellationToken);
                return RedirectToAction(nameof(Index));
            }

        }
        [HttpPost]
        public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            await _paymentHistoryRepository.DeletePaymentHistoryAsync(id, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Details(long id, CancellationToken cancellationToken)
        {
            var data = await _paymentHistoryRepository.GetPaymentHistoryByIdAsync(id, cancellationToken);
            if (data != null)
            {
                return View(data);
            }
            return NotFound();
        }
    }
}
