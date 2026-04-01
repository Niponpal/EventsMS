namespace EventsMS.Models
{
    public class CongratulationsViewModel
    {
        public StudentRegistration Registration { get; set; } = default!;
        public Payment? Payment { get; set; } // optional
    }
}
