using System.ComponentModel.DataAnnotations;

namespace FU_House_Finder.Models
{
    public class Campus
    {
        // Attributes for DTO
        [Key]
        [Required]
        public int CampusID { get; set; }

        [MaxLength(100)]
        [Required]
        public string CampusName { get; set; } = string.Empty;
        // =====================================================


        // Hidden Attributes
        [Required]
        public DateTime CreatedDate { get; set; }
        // =====================================================


        // Foreign Key Attributes

        // 04 | 1 Campus => 0 - many Houses | FK: CampusID
        public ICollection<House>? Houses { get; set; }

        // 05 | 1 Address => 1 - many Campuses | FK: AddressID
        public Address Address { get; set; } = null!;
        public int AdressID { get; set; }

        // =====================================================


    }
}
