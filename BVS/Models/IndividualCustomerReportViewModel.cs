using System.Collections.Generic;

namespace BVS.Models
{
    public class IndividualCustomerReportViewModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public List<string> CurrentRentals { get; set; } = new();
        public List<string> PastRentals { get; set; } = new();
    }
}
