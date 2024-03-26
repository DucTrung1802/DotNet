using System.ComponentModel.DataAnnotations;

namespace FU_House_Finder.Models
{
    public class Status
    {
        // Attributes for DTO
        [Key]
        [Required]
        public int StatusID { get; set; }

        [MaxLength(30)]
        [Required]
        public string StatusName { get; set; } = string.Empty;
        // =====================================================


        // Hidden Attributes
        [Required]
        public DateTime CreatedDate { get; set; }
        // =====================================================


        // Foreign Key Attributes

        // =====================================================
    }
}
