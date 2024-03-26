using System.ComponentModel.DataAnnotations;

namespace FU_House_Finder.Models
{
    public class RoomType
    {
        // Attributes for DTO
        [Key]
        [Required]
        public int RoomTypeID { get; set; }

        [MaxLength(20)]
        [Required]
        public string RoomTypeName { get; set; } = string.Empty;
        // =====================================================


        // Hidden Attributes
        [Required]
        public DateTime CreatedDate { get; set; }
        // =====================================================


        // Foreign Key Attributes

        // =====================================================
    }
}
