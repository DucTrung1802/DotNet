using System.ComponentModel.DataAnnotations;

namespace FU_House_Finder.Models
{
    public class Address
    {
        // Attributes for DTO
        [Key]
        [Required]
        public int AddressID { get; set; }

        [Required]
        public string Addresses { get; set; } = string.Empty;

        [Required]
        public string GoogleMapLocation { get; set; } = string.Empty;
        // =====================================================


        // Hidden Attributes
        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastModifiedDate { get; set; }
        // =====================================================


        // Foreign Key Attributes
        // empty
        // =====================================================
    }
}
