using Microsoft.Extensions.Logging;

namespace EventsMS.Models;

public class Category:BaseEntities.BaseEntity<long>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    // Navigation property
    public ICollection<Event> Events { get; set; } = new List<Event>();
}
