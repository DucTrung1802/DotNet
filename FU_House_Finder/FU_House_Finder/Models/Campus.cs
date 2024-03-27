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

        // =====================================================


    }
}
