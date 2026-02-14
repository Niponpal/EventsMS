using EventsMS.Models;
using EventsMS.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EventsMS.Controllers;

public class EventController : Controller
{

    private readonly IEventRepository _eventRepository;

    public EventController(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
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

    public async Task<IActionResult> CreateOrEdit(long id, CancellationToken cancellationToken)
    {
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
    public async Task<IActionResult> CreateOrEdit(Event events, CancellationToken cancellationToken)
    {
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

}
