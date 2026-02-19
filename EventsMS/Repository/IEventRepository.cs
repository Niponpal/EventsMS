using EventsMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventsMS.Repository;

public interface IEventRepository
{
    Task<IEnumerable<Event>> GetAllEventAsync(CancellationToken cancellationToken);
    Task<Event?> GeEventByIdAsync(long id, CancellationToken cancellationToken);
    Task<Event> AddEventAsync(Event events, CancellationToken cancellationToken);
    Task<Event?> UpdateEventAsync(Event events, CancellationToken cancellationToken);
    Task<Event> DeleteEventyAsync(long id, CancellationToken cancellationToken);

    IEnumerable<SelectListItem> Dropdown();
}
