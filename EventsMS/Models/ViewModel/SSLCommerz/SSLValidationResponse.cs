namespace EventsMS.Models.ViewModel.SSLCommerz
{
    public class SSLValidationResponse
    {
        public string status { get; set; }
        public string tran_id { get; set; }
        public string amount { get; set; }
        public string cus_name { get; set; }

        // Custom fields
        public string value_a { get; set; } // BillId
        public string value_b { get; set; } // MealCycleId
    }
}
