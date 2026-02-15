namespace EventsMS.Models;

public class Payment:BaseEntities.BaseEntity<long>
{
    public string InvoiceNumber { get; set; } = default!;
    public string Provider { get; set; } = "Stripe";
    public string Status { get; set; } = "Pending";
    public decimal Amount { get; set; }
    public PaymentHistory PaymentHistory { get; set; }
    public long RegistrationId { get; set; }
    public StudentRegistration Registration { get; set; }
    public string TransactionId { get; set; } = string.Empty;
    public string ValidationId { get; set; } = string.Empty;
    public string ProviderSessionId { get; set; } = default!;

}
