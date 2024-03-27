using System.ComponentModel.DataAnnotations;

namespace FU_House_Finder.Models
{
    public class Report
    {
        // Attributes for DTO
        [Key]
        [Required]
        public int ReportID { get; set; }

        [Required]
        public string ReportContent { get; set; } = string.Empty;
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
