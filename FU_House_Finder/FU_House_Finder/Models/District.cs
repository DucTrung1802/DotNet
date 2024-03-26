using System.ComponentModel.DataAnnotations;

namespace FU_House_Finder.Models
{
    public class District
    {
        // Attributes for DTO
        [Key]
        [Required]
        public int DistrictID { get; set; }

        [MaxLength(40)]
        [Required]
        public string DistrictName { get; set; } = string.Empty;
        // =====================================================

        // Hidden Attributes
        public DateTime CreatedDate { get; set; }
        // =====================================================

        // Foreign Key Attributes
        public ICollection<Commune> Communes { get; set; } = new List<Commune>();
        // =====================================================

    }
}