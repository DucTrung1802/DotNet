using System.ComponentModel.DataAnnotations;

namespace FU_House_Finder.Models
{
    public class Commune
    {
        // Attributes for DTO 
        [Key]
        [Required]
        public int CommuneID { get; set; }

        [MaxLength(40)]
        [Required]
        public string CommuneName { get; set; } = string.Empty;
        // =====================================================

        // Hidden Attributes
        [Required]
        public DateTime CreatedDate { get; set; }
        // =====================================================

        // Foreign Key Attributes

        // 01 | 1 District => 1 -> many Communes | FK: DistrictID
        public District District { get; set; } = null!;
        public int DistrictID { get; set; }

        // 02 | 1 Commune => 1 -> many Villages | FK: CommuneID
        public ICollection<Village> Villages { get; set; } = new List<Village>();

        // =====================================================
    }
}
