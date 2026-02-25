using EventsMS.Data;
using EventsMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EventsMS.Repository;

public class EventRepository : IEventRepository
{
   private readonly ApplicationDbContext _context;
    public EventRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Event> AddEventAsync(Event events, CancellationToken cancellationToken)
    {
        var data = await _context.Events.AddAsync(events, cancellationToken);
        if (data != null)
        {
            await _context.SaveChangesAsync(cancellationToken);
            return events;
        }
        return null;
    }

    public async Task<Event> DeleteEventyAsync(long id, CancellationToken cancellationToken)
    {
        var data = await _context.Events.FindAsync(id, cancellationToken); 
        if (data != null)
        {
            _context.Events.Remove(data);
            await _context.SaveChangesAsync(cancellationToken);
            return data;
        }
        return null;
    }

    public async Task<Event?> GeEventByIdAsync(long id, CancellationToken cancellationToken)
    {
       var data= await _context.Events.FindAsync(id, cancellationToken);
        if (data != null)
        {
            return data;
        }
        return null;
    }

    public async Task<IEnumerable<Event>> GetAllEventAsync(CancellationToken cancellationToken)
    {
        var data = await _context.Events.ToListAsync(cancellationToken);
        if (data != null)
        {
            return data;
        }
        return null;
    }

    public async Task<Event?> UpdateEventAsync(Event events, CancellationToken cancellationToken)
    {
        var data = await _context.Events.FindAsync(events.Id, cancellationToken);
        if (data != null)
        {
            data.Name = events.Name;
            data.Description = events.Description;
            data.StartDate = events.StartDate;
            data.EndDate = events.EndDate;
            data.RegistrationFee = events.RegistrationFee;
            data.Slug = events.Slug;
            data.ImageUrl = events.ImageUrl; 
            data.MealsOffered = events.MealsOffered;
            data.IsFree = events.IsFree;
            await _context.SaveChangesAsync(cancellationToken);
            return data;
        }
        return null;
    }

    public IEnumerable<SelectListItem> Dropdown()
    {
        var data = _context.Events.Select(x => new SelectListItem
        {
            Text = x.Name,
            Value = x.Id.ToString()
        }).ToList();
        return data;
    }
}
