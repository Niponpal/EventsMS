namespace EventsMS.Models
{
    public class StudentRegistration : BaseEntities.BaseEntity<long>
    {
        public string FullName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string IdCardNumber { get; set; } = default!;
        public string Department { get; set; } = default!;
        public string PhotoPath { get; set; } = default!;

        // PaymentStatus can be "Pending", "Completed", "Failed"
        public string PaymentStatus { get; set; } = "Pending";

        public long EventId { get; set; }
        public Event Event { get; set; } = default!;

        public long UserId { get; set; }

        public Payment Payment { get; set; }

        // Only collection, remove single FoodToken property
        public ICollection<FoodToken> foodTokens { get; set; } = new List<FoodToken>();
    }
}