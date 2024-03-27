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

        // 05 | 1 Address => 1 - many Campuses | FK: AddressID
        public ICollection<Campus> Campuses { get; set; } = new List<Campus>();

        // 06 | 1 Adress => 1 House | FK: AddressID
        public House House { get; set; } = null!;

        // =====================================================
    }
}
