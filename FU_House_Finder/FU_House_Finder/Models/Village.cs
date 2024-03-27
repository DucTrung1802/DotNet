using System.ComponentModel.DataAnnotations;

namespace FU_House_Finder.Models
{
    public class Village
    {
        // Attributes for DTO
        [Key]
        [Required]
        public int VillageID { get; set; }

        [MaxLength(40)]
        [Required]
        public string VillageName { get; set; } = string.Empty;
        // =====================================================


        // Hidden Attributes
        [Required]
        public DateTime CreatedDate { get; set; }
        // =====================================================


        // Foreign Key Attributes

        // 02 | 1 Commune => 1 -> many Villages | FK: CommuneID
        public Commune Commune { get; set; } = null!;
        public int CommuneID { get; set; }

        // 03 | 1 Village => 0 -> many Houses | FK: VillageID
        public ICollection<House>? Houses { get; set; }
        // =====================================================
    }
}
