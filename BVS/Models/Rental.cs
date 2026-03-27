namespace BVS.Models
{
    public class Rental
    {
        public int RentalId { get; set; }

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public int VideoId { get; set; }
        public Video? Video { get; set; }

        public DateTime RentedDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime DueDate { get; set; }
        
        public required string Status { get; set; }
        public decimal Penalty { get; set; }
        // Price charged for this rental (25 for VCD, 50 for DVD)
        public decimal Price { get; set; }
    }
}
