namespace BVS.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public required string FullName { get; set; }
        public required string Contact { get; set; }
    }
}
