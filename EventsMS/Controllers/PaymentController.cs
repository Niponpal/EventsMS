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
    //[HttpGet]
    //public async Task<IActionResult> CreateOrEdit(long  id, CancellationToken cancellationToken)
    //{
    //    ViewData["RegistrationId"] = _studentRegistrationRepository.Dropdown();
    //    if (id == 0)
    //    {
    //        return View(new Payment());
    //    }
    //    else
    //    {
    //        var data = await _paymentRepository.GetPaymentByIdAsync(id, cancellationToken);
    //        if (data != null)
    //        {
    //            return View(data);
    //        }
    //        return NotFound();
    //    }
    //}

    [HttpGet]
    public async Task<IActionResult> CreateOrEdit(long id, long? registrationId, CancellationToken cancellationToken)
    {
        ViewData["RegistrationId"] = _studentRegistrationRepository.Dropdown();
        if (id == 0)
        {
            return View(new Payment { RegistrationId = registrationId ?? 0 });
        }
        else
        {
            var data = await _paymentRepository.GetPaymentByIdAsync(id, cancellationToken);
            if (data != null) return View(data);
            return NotFound();
        }
    }
    //[HttpPost]
    //public async Task<IActionResult> CreateOrEdit(Payment payment, CancellationToken cancellationToken)
    //{
    //    ViewData["RegistrationId"] = _studentRegistrationRepository.Dropdown();
    //    if (payment.Id == 0)
    //    {
    //        await _paymentRepository.AddPaymentAsync(payment, cancellationToken);
    //        return RedirectToAction(nameof(Index));
    //    }
    //    else
    //    {
    //        await _paymentRepository.UpdatePaymentAsync(payment, cancellationToken);
    //        return RedirectToAction(nameof(Index));
    //    }
    //}

    [HttpPost]
    public async Task<IActionResult> CreateOrEdit(Payment payment, CancellationToken cancellationToken)
    {
        ViewData["RegistrationId"] = _studentRegistrationRepository.Dropdown();

        if (payment.Id == 0)
            await _paymentRepository.AddPaymentAsync(payment, cancellationToken);
        else
            await _paymentRepository.UpdatePaymentAsync(payment, cancellationToken);

        // 🔹 Redirect to Congratulations after payment
        return RedirectToAction("Congratulations", "Studentregistration", new { registrationId = payment.RegistrationId });
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


    [HttpGet]
    public async Task<IActionResult> DownloadReceipt(long id, CancellationToken cancellationToken = default)
    {
        var payment = await _paymentRepository.GetPaymentByIdAsync(id, cancellationToken);
        if (payment == null)
            return NotFound();

        // Professional receipt content (you can enhance this with HTML + PDF library later)
        var receiptContent = $@"
Payment Receipt
====================================
Invoice Number     : #{payment.InvoiceNumber}
Student Name       : {payment.Registration?.FullName ?? "N/A"}
Event              : {payment.Registration?.Event?.Name ?? "N/A"}  // Assuming navigation property
Amount             : {payment.Amount:C2}
Status             : {payment.Status}
Payment Provider   : {payment.Provider}
Transaction ID     : {payment.TransactionId ?? "—"}
Validation ID      : {payment.ValidationId ?? "—"}
Date               : {DateTime.Now:dd MMMM yyyy HH:mm:ss}
====================================
Thank you for your payment!
            ".Trim();

        var fileBytes = System.Text.Encoding.UTF8.GetBytes(receiptContent);

        return File(
            fileBytes,
            "application/octet-stream",           // Change to "application/pdf" when using real PDF
            $"Receipt_{payment.InvoiceNumber}_{DateTime.Now:yyyyMMdd}.txt"
        );
    }

}
