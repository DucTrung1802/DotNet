using System.ComponentModel.DataAnnotations;

namespace FU_House_Finder.Models
{
    public class RoomImage
    {
        // Attributes for DTO
        [Key]
        [Required]
        public int ImageID { get; set; }

        [Required]
        public string ImageLink { get; set; } = string.Empty;
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
