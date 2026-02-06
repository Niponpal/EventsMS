namespace EventsMS.Models;

public class Registration
{
    public Guid Id { get; set; }

    // Foreign key for Event
    public Guid EventId { get; set; }
    public Event Event { get; set; } = null!;

    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string? Department { get; set; }
    public decimal Amount { get; set; }
    public string? Payment { get; set; }

    // Navigation property for PaymentHistory
    public ICollection<PaymentHistory> PaymentHistories { get; set; } = new List<PaymentHistory>();
}
