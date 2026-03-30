using EventsMS.FileServices;
using EventsMS.Models;
using EventsMS.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace EventsMS.Controllers;

public class StudentregistrationController : Controller
{
    private readonly IStudentRegistrationRepository _studentRegistrationRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IFileService _fileService;



    public StudentregistrationController(IStudentRegistrationRepository studentRegistrationRepository, IEventRepository eventRepository, IFileService fileService)
    {
        _studentRegistrationRepository = studentRegistrationRepository;
        _eventRepository = eventRepository;
        _fileService = fileService;
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
    //public async Task<IActionResult> CreateOrEdit(long id, CancellationToken cancellationToken)
    //{
    //    ViewData["EventId"] = _eventRepository.Dropdown();
    //    if (id == 0)
    //        return View(new StudentRegistration()); // নতুন রেজিস্ট্রেশন
    //    else
    //    {
    //        var data = await _studentRegistrationRepository.GetStudentRegistrationByIdAsync(id, cancellationToken);
    //        if (data != null)
    //            return View(data); // Edit view
    //        return NotFound();
    //    }
    //}

    [HttpGet]
    public async Task<IActionResult> CreateOrEdit(long id, long? eventId, CancellationToken cancellationToken)
    {
        StudentRegistration model;

        if (id == 0)
        {
            // নতুন রেজিস্ট্রেশন
            model = new StudentRegistration();

            if (eventId != null)
            {
                model.EventId = eventId.Value;
            }
        }
        else
        {
            // Edit mode
            model = await _studentRegistrationRepository.GetStudentRegistrationByIdAsync(id, cancellationToken);

            if (model == null)
                return NotFound();
        }

        // selected event fetch
        long selectedEventId = model.EventId != 0 ? model.EventId : (eventId ?? 0);

        if (selectedEventId != 0)
        {
            var selectedEvent = await _eventRepository.GeEventByIdAsync(selectedEventId, cancellationToken);

            if (selectedEvent != null)
            {
                ViewData["EventId"] = new SelectList(
                    new[] { new { Id = selectedEvent.Id, Name = selectedEvent.Name } }, // শুধু selected event
                    "Id",
                    "Name",
                    selectedEvent.Id
                );
            }
        }
        else
        {
            ViewData["EventId"] = null; // যদি কোন event na select হয়
        }

        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> CreateOrEdit(StudentRegistration studentRegistration, IFormFile photo, CancellationToken cancellationToken)
    {
        if (photo != null)
        {
            var fileName = await _fileService.Upload(photo, "StudentPhotos");
            studentRegistration.PhotoPath = fileName;
        }
        else
        {
            // যদি photo upload না করা হয়
            studentRegistration.PhotoPath ??= "default.png"; 
        }

        if (studentRegistration.Id == 0)
            await _studentRegistrationRepository.AddStudentRegistrationAsync(studentRegistration, cancellationToken);
        else
            await _studentRegistrationRepository.UpdateStudentRegistrationAsync(studentRegistration, cancellationToken);

        return RedirectToAction("Index", "Home");
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

}
