namespace EventsMS.Models;

public class PaymentHistory
{
    public Guid Id { get; set; }

    public string InvoiceNumber { get; set; } = null!;
    public string StudentIdCard { get; set; } = null!;
    public string StudentName { get; set; } = null!;
    public string Email { get; set; } = null!;

    // Foreign key for Event
    public Guid EventId { get; set; }
    public Event Event { get; set; } = null!;

    public decimal PaidAmount { get; set; }
    public string BankName { get; set; } = null!;
    public string TransactionId { get; set; } = null!;
    public DateTime PaymentDate { get; set; }
    public string Status { get; set; } = null!;
}
