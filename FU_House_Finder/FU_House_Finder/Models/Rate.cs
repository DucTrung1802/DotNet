using System.ComponentModel.DataAnnotations;

namespace FU_House_Finder.Models
{
    public class Rate
    {
        // Attributes for DTO
        [Key]
        [Required]
        public int RateID { get; set; }

        [Required]
        public double Star { get; set; }

        [Required]
        public string Comment { get; set; } = string.Empty;

        [Required]
        public string LandlordReply { get; set; } = string.Empty;
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
