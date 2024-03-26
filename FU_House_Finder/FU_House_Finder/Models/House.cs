using System.ComponentModel.DataAnnotations;

namespace FU_House_Finder.Models
{
    public class House
    {
        // Attributes for DTO
        [Key]
        [Required]
        public int HouseID { get; set; }

        [MaxLength(100)]
        [Required]
        public string HouseName { get; set; } = string.Empty;

        [MaxLength(400)]
        [Required]
        public string Information { get; set; } = string.Empty;

        [Required]
        public double PowerPrice { get; set; }

        [Required]
        public double WaterPrice { get; set; }
        // =====================================================


        // Hidden Attributes
        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastModifiedDate { get; set; }
        // =====================================================


        // Foreign Key Attributes

        // =====================================================
    }
}
