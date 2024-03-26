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
        public Commune Commune { get; set; } = null!;

        public int CommuneID { get; set; }
        // =====================================================
    }
}
