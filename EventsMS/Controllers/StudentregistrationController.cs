using EventsMS.FileServices;
using EventsMS.Models;
using EventsMS.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EventsMS.Controllers;

public class StudentregistrationController : Controller
{
    private readonly IStudentRegistrationRepository _studentRegistrationRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IFileService _fileService;
    private readonly IPaymentRepository _paymentRepository;


    public StudentregistrationController(IStudentRegistrationRepository studentRegistrationRepository, IEventRepository eventRepository, IPaymentRepository paymentRepository,IFileService fileService)
    {
        _studentRegistrationRepository = studentRegistrationRepository;
        _eventRepository = eventRepository;
        _fileService = fileService;
        _paymentRepository = paymentRepository;

    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        
        var data = await _studentRegistrationRepository.GetAllStudentRegistrationAsync(cancellationToken);
        if (data != null)
        {
            return View(data);
        }
        return NotFound();
    }




    //[HttpGet]
    //public async Task<IActionResult> CreateOrEdit(long id, long? eventId, CancellationToken cancellationToken)
    //{
    //    if (!User.Identity.IsAuthenticated)
    //    {
    //        return RedirectToAction("Login", "Account", new
    //        {
    //            returnUrl = Url.Action("CreateOrEdit", "Studentregistration", new { id, eventId })
    //        });
    //    }

    //    StudentRegistration model;

    //    if (id == 0)
    //    {
    //        model = new StudentRegistration();
    //        if (eventId != null) model.EventId = eventId.Value;
    //    }
    //    else
    //    {
    //        model = await _studentRegistrationRepository.GetStudentRegistrationByIdAsync(id, cancellationToken);
    //        if (model == null) return NotFound();
    //    }

    //    long selectedEventId = model.EventId != 0 ? model.EventId : (eventId ?? 0);

    //    if (selectedEventId != 0)
    //    {
    //        var selectedEvent = await _eventRepository.GeEventByIdAsync(selectedEventId, cancellationToken);
    //        if (selectedEvent != null)
    //        {
    //            ViewData["EventId"] = new SelectList(
    //                new[] { new { Id = selectedEvent.Id, Name = selectedEvent.Name } },
    //                "Id",
    //                "Name",
    //                selectedEvent.Id
    //            );
    //        }
    //    }
    //    else
    //    {
    //        ViewData["EventId"] = null;
    //    }

    //    return View(model);
    //}

    [HttpGet]
    public async Task<IActionResult> CreateOrEdit(long id, long? eventId, CancellationToken cancellationToken)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Account", new
            {
                returnUrl = Url.Action("CreateOrEdit", "Studentregistration", new { id, eventId })
            });
        }

        StudentRegistration model;

        if (id == 0)
        {
            model = new StudentRegistration();
            if (eventId != null) model.EventId = eventId.Value;

            // ✅ Auto-fill email of logged-in user
            var emailClaim = User.FindFirst(ClaimTypes.Email) ?? User.FindFirst(ClaimTypes.Name);
            if (emailClaim != null)
                model.Email = emailClaim.Value;
        }
        else
        {
            model = await _studentRegistrationRepository.GetStudentRegistrationByIdAsync(id, cancellationToken);
            if (model == null) return NotFound();
        }

        long selectedEventId = model.EventId != 0 ? model.EventId : (eventId ?? 0);

        if (selectedEventId != 0)
        {
            var selectedEvent = await _eventRepository.GeEventByIdAsync(selectedEventId, cancellationToken);
            if (selectedEvent != null)
            {
                ViewData["EventId"] = new SelectList(
                    new[] { new { Id = selectedEvent.Id, Name = selectedEvent.Name } },
                    "Id",
                    "Name",
                    selectedEvent.Id
                );
            }
        }
        else
        {
            ViewData["EventId"] = null;
        }

        return View(model);
    }




    //[HttpPost]
    //public async Task<IActionResult> CreateOrEdit(StudentRegistration studentRegistration, IFormFile photo, CancellationToken cancellationToken)
    //{
    //    if (photo != null)
    //    {
    //        var fileName = await _fileService.Upload(photo, "StudentPhotos");
    //        studentRegistration.PhotoPath = fileName;
    //    }
    //    else
    //    {
    //        studentRegistration.PhotoPath ??= "default.png";
    //    }

    //    if (studentRegistration.Id == 0)
    //        await _studentRegistrationRepository.AddStudentRegistrationAsync(studentRegistration, cancellationToken);
    //    else
    //        await _studentRegistrationRepository.UpdateStudentRegistrationAsync(studentRegistration, cancellationToken);

    //    // 🔹 Free/Paid redirect logic
    //    var ev = await _eventRepository.GeEventByIdAsync(studentRegistration.EventId, cancellationToken);
    //    if (ev == null) return NotFound();

    //    if (ev.IsFree)
    //    {
    //        // Free event → congratulations page
    //        return RedirectToAction("Congratulations", new { registrationId = studentRegistration.Id });
    //    }
    //    else
    //    {
    //        // Paid event → payment page
    //        return RedirectToAction("CreateOrEdit", "Payment", new { id = 0, registrationId = studentRegistration.Id });
    //    }
    //}


    [HttpPost]
    public async Task<IActionResult> CreateOrEdit(StudentRegistration studentRegistration, IFormFile photo, CancellationToken cancellationToken)
    {
        // 🔴 Email already exists check
        var exists = await _studentRegistrationRepository
            .IsEmailAlreadyRegistered(studentRegistration.Email, studentRegistration.EventId, cancellationToken);

        if (exists)
        {
            TempData["Error"] = "This email is already used for this event!";
            return RedirectToAction("CreateOrEdit", new { id = 0, eventId = studentRegistration.EventId });
        }

        // ✅ Image upload
        if (photo != null)
        {
            var fileName = await _fileService.Upload(photo, "StudentPhotos");
            studentRegistration.PhotoPath = fileName;
        }
        else
        {
            studentRegistration.PhotoPath ??= "default.png";
        }

        // ✅ Save
        if (studentRegistration.Id == 0)
            await _studentRegistrationRepository.AddStudentRegistrationAsync(studentRegistration, cancellationToken);
        else
            await _studentRegistrationRepository.UpdateStudentRegistrationAsync(studentRegistration, cancellationToken);

        // ✅ Success popup message
        TempData["Success"] = "Registration Completed Successfully!";

        // 🔹 Free/Paid redirect
        var ev = await _eventRepository.GeEventByIdAsync(studentRegistration.EventId, cancellationToken);
        if (ev == null) return NotFound();

        if (ev.IsFree)
        {
            return RedirectToAction("Congratulations", new { registrationId = studentRegistration.Id });
        }
        else
        {
            return RedirectToAction("CreateOrEdit", "Payment", new { id = 0, registrationId = studentRegistration.Id });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        await _studentRegistrationRepository.DeleteStudentRegistrationAsync(id, cancellationToken);
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Details(long id, CancellationToken cancellationToken)
    {
        var data = await _studentRegistrationRepository.GetStudentRegistrationByIdAsync(id, cancellationToken);
        if (data != null)
        {
            return View(data);
        }
        return NotFound();
    }



    [HttpGet]
    public async Task<IActionResult> Congratulations(long registrationId, CancellationToken cancellationToken)
    {
        var registration = await _studentRegistrationRepository.GetStudentRegistrationByIdAsync(registrationId, cancellationToken);
        if (registration == null)
            return NotFound();

        // Event name
        string eventName = string.Empty;
        if (registration.EventId != 0)
        {
            var evnt = await _eventRepository.GeEventByIdAsync(registration.EventId, cancellationToken);
            if (evnt != null)
                eventName = evnt.Name;
        }
        ViewData["EventName"] = eventName;

        // Payment info (if exists)
        var payment = await _paymentRepository.GetPaymentByRegistrationIdAsync(registrationId, cancellationToken);

        var vm = new CongratulationsViewModel
        {
            Registration = registration,
            Payment = payment
        };

        return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> MyEvents(CancellationToken cancellationToken)
    {
        if (!User.Identity.IsAuthenticated)
            return RedirectToAction("Login", "Account");

        var emailClaim = User.FindFirst(ClaimTypes.Email) ?? User.FindFirst(ClaimTypes.Name);
        if (emailClaim == null) return Unauthorized();

        // Debug
        ViewData["Debug"] = $"Claim Email: '{emailClaim.Value}'";

        var myEvents = await _studentRegistrationRepository.GetMyRegistrationsAsync(emailClaim.Value, cancellationToken);

        if (!myEvents.Any())
            ViewData["Debug"] += " → No registrations found in DB!";

        return View(myEvents ?? new List<StudentRegistration>());
    }



}
