using System.ComponentModel.DataAnnotations;

namespace FU_House_Finder.Models
{
    public class User
    {
        // Attributes for DTO
        [Key]
        [Required]
        public int UserID { get; set; }

        [Required]
        public string FacebookUserID { get; set; } = string.Empty;

        [Required]
        public string GoogleUserID { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string DisplayName { get; set; } = string.Empty;

        [Required]
        public bool Active { get; set; }

        [Required]
        public string ProfileImageLink { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string FacebookURL { get; set; } = string.Empty;

        [Required]
        public string IdentityCardFrontSideImageLink { get; set; } = string.Empty;

        [Required]
        public string IdentityCardBackSideImageLink { get; set; } = string.Empty;
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
