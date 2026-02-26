using EventsMS.FileServices;
using EventsMS.Models;
using EventsMS.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EventsMS.Controllers;

public class EventController : Controller
{

    private readonly IEventRepository _eventRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IFileService _fileService;
   
    public EventController(IEventRepository eventRepository, ICategoryRepository categoryRepository, IFileService fileService)
    {
        _eventRepository = eventRepository;
        _categoryRepository = categoryRepository;
        _fileService = fileService;
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
    public async Task<IActionResult> CreateOrEdit(
       Event events,
       IFormFile? imageFile,
       CancellationToken cancellationToken)
    {
        ViewData["CategoryId"] = _categoryRepository.Dropdown();

      

        // CREATE
        if (events.Id == 0)
        {
            if (imageFile != null)
            {
                var fileName = await _fileService.Upload(imageFile, "images/events");
                events.ImageUrl = fileName;
            }

            await _eventRepository.AddEventAsync(events, cancellationToken);
        }
        else
        {
            var existingEvent = await _eventRepository.GeEventByIdAsync(events.Id, cancellationToken);

            if (existingEvent == null)
                return NotFound();

            if (imageFile != null)
            {
                // delete old image
                if (!string.IsNullOrEmpty(existingEvent.ImageUrl))
                {
                    _fileService.DeleteFile(existingEvent.ImageUrl, "images/events");
                }

                var fileName = await _fileService.Upload(imageFile, "images/events");
                events.ImageUrl = fileName;
            }
            else
            {
                events.ImageUrl = existingEvent.ImageUrl;
            }

            await _eventRepository.UpdateEventAsync(events, cancellationToken);
        }

        return RedirectToAction(nameof(Index));
    }




    [HttpPost]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        var existingEvent = await _eventRepository.GeEventByIdAsync(id, cancellationToken);

        if (existingEvent != null && !string.IsNullOrEmpty(existingEvent.ImageUrl))
        {
            _fileService.DeleteFile(existingEvent.ImageUrl, "images/events");
        }

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
