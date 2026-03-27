using System.Collections.Generic;

namespace BVS.Models
{
    public class CustomerRentalsViewModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public List<string> CurrentRentals { get; set; } = new();
    }
}
