using System.ComponentModel.DataAnnotations;

namespace FU_House_Finder.Models
{
    public class UserRole
    {
        // Attributes for DTO
        [Key]
        [Required]
        public int RoleID { get; set; }

        [Required]
        public string RoleName { get; set; } = string.Empty;
        // =====================================================


        // Hidden Attributes
        [Required]
        public DateTime CreatedDate { get; set; }
        // =====================================================


        // Foreign Key Attributes
        // empty
        // =====================================================
    }
}
