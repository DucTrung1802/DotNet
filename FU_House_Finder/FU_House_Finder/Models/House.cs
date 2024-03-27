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

        // 03 | 1 Village => 0 -> many Houses | FK: VillageID
        public Village Village { get; set; } = null!;
        public int VillageID { get; set; }

        // 04 | 1 Campus => 0 - many Houses | FK: CampusID
        public Campus Campus { get; set; } = null!;
        public int CampusID { get; set; }

        // 06 | 1 Adress => 1 House | FK: AddressID, HouseID
        public Address Address { get; set; } = null!;
        public int AddressID { get; set; }

        // =====================================================
    }
}
