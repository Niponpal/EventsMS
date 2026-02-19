using EventsMS.Models;
using EventsMS.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EventsMS.Controllers;

public class StudentregistrationController : Controller
{
    private readonly IStudentRegistrationRepository _studentRegistrationRepository;
    private readonly IEventRepository _eventRepository;



    public StudentregistrationController(IStudentRegistrationRepository studentRegistrationRepository, IEventRepository eventRepository)
    {
        _studentRegistrationRepository = studentRegistrationRepository;
        _eventRepository = eventRepository;
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
    [HttpGet]
    public async Task<IActionResult> CreateOrEdit(long id, CancellationToken cancellationToken)
    {
        ViewData["EventId"] = _eventRepository.Dropdown();
        if (id == 0)
        {
            return View(new StudentRegistration());
        }
        else
        {
            var data = await _studentRegistrationRepository.GetStudentRegistrationByIdAsync(id, cancellationToken);
            if (data != null)
            {
                return View(data);
            }
            return NotFound();
        }
    }
    [HttpPost]
    public async Task<IActionResult> CreateOrEdit(StudentRegistration studentRegistration, CancellationToken cancellationToken)
    {
        if (studentRegistration.Id == 0)
            {
                await _studentRegistrationRepository.AddStudentRegistrationAsync(studentRegistration, cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                await _studentRegistrationRepository.UpdateStudentRegistrationAsync(studentRegistration, cancellationToken);
                return RedirectToAction(nameof(Index));
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

}
