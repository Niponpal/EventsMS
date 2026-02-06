namespace EventsMS.Models;

public class Event
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Image { get; set; }

    // Foreign key for Category
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Fee { get; set; }
    public string? MealType { get; set; }

    // Navigation property
    public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}
