using EventsMS.Models;
using EventsMS.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EventsMS.Controllers;

public class PaymentController : Controller
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IStudentRegistrationRepository _studentRegistrationRepository;

    public PaymentController(IPaymentRepository paymentRepository, IStudentRegistrationRepository studentRegistrationRepository)
    {
        _paymentRepository = paymentRepository;
        _studentRegistrationRepository = studentRegistrationRepository;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var data = await _paymentRepository.GetAllPaymentAsync(cancellationToken);
        if (data != null)
        {
            return View(data);
        }
    
        return NotFound();
    }
    [HttpGet]
    public async Task<IActionResult> CreateOrEdit(long  id, CancellationToken cancellationToken)
    {
        ViewData["RegistrationId"] = _studentRegistrationRepository.Dropdown();
        if (id == 0)
        {
            return View(new Payment());
        }
        else
        {
            var data = await _paymentRepository.GetPaymentByIdAsync(id, cancellationToken);
            if (data != null)
            {
                return View(data);
            }
            return NotFound();
        }
    }
    [HttpPost]
    public async Task<IActionResult> CreateOrEdit(Payment payment, CancellationToken cancellationToken)
    {
        ViewData["RegistrationId"] = _studentRegistrationRepository.Dropdown();
        if (payment.Id == 0)
        {
            await _paymentRepository.AddPaymentAsync(payment, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
        else
        {
            await _paymentRepository.UpdatePaymentAsync(payment, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
    }
    [HttpGet]
    public async Task<IActionResult> Details(long id, CancellationToken cancellationToken)
    {
        var data = await _paymentRepository.GetPaymentByIdAsync(id, cancellationToken);
        if (data != null)
        {
            return View(data);
        }
        return NotFound();
    }
    [HttpPost]
        public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            await _paymentRepository.DeletePaymentyAsync(id, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
}
