namespace EventsMS.Models
{
    public class Payment
    {
        public string InvoiceNumber { get; set; } = default!;
        public long RegistrationId { get; set; }
        public StudentRegistration Registration { get; set; }
        public string Provider { get; set; } = "Stripe";
        public string ProviderSessionId { get; set; } = default!;
        public string Status { get; set; } = "Pending";
        public string TransactionId { get; set; } = string.Empty;
        public string ValidationId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public PaymentHistory PaymentHistory { get; set; }

    }
}
