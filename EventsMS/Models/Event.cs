namespace EventsMS.Models;

public class Event:BaseEntities.BaseEntity<long>
{
    public string ImageUrl { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; }
    public DateTimeOffset StartDate { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset EndDate { get; set; } = DateTimeOffset.UtcNow;
    public decimal RegistrationFee { get; set; }
    public string Slug { get; set; } = default!;
    public int MealsOffered { get; set; }
    public bool IsFree { get; set; }
    public long CategoryId { get; set; }
    public Category Category { get; set; }
    public ICollection<StudentRegistration> Registrations { get; set; } = new List<StudentRegistration>();
}
