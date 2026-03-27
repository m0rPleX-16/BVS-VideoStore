using System.ComponentModel.DataAnnotations;

namespace BVS.Models
{
    public enum CategoryType
    {
        VCD,
        DVD
    }

    public class Video
    {
        public int VideoId { get; set; }

        [Required]
        public required string Title { get; set; }

        [Required]
        public CategoryType Category { get; set; }

        [Range(0, int.MaxValue)]
        public int AvailableQuantity { get; set; }

        [Range(0, int.MaxValue)]
        [Required]
        public int TotalQuantity { get; set; }

        [Range(1, 3)]
        [Required]
        public int RentalDays { get; set; }
    }
}
