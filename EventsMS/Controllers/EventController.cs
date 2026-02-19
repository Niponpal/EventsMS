using EventsMS.Models;
using EventsMS.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EventsMS.Controllers;

public class EventController : Controller
{

    private readonly IEventRepository _eventRepository;
    private readonly ICategoryRepository _categoryRepository;

   

    public EventController(IEventRepository eventRepository, ICategoryRepository categoryRepository)
    {
        _eventRepository = eventRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {

        var data = await _eventRepository.GetAllEventAsync(cancellationToken);
        if (data != null)
        {
            return View(data);
        }
        return View(NotFound());
    }
    [HttpGet]
    public async Task<IActionResult> CreateOrEdit(long id, CancellationToken cancellationToken)
    {
        ViewData["CategoryId"] = _categoryRepository.Dropdown();
        if (id == 0)
        {
            return View(new Models.Event());
        }
        var data = await _eventRepository.GeEventByIdAsync(id, cancellationToken);
        if (data != null)
        {
            return View(data);
        }
        return View(NotFound());
    }
    [HttpPost]
    public async Task<IActionResult> CreateOrEdit(Event events, CancellationToken cancellationToken)
    {
        ViewData["CategoryId"] = _categoryRepository.Dropdown();
        if (events.Id == 0)
        {
            await _eventRepository.AddEventAsync(events, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
        else
        {
            await _eventRepository.UpdateEventAsync(events, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
        
    }
    [HttpPost]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        await _eventRepository.DeleteEventyAsync(id, cancellationToken);
        return RedirectToAction(nameof(Index));
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

}
