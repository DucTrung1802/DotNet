using System.ComponentModel.DataAnnotations;

namespace FU_House_Finder.Models
{
    public class RoomHistory
    {
        // Attributes for DTO
        [Key]
        [Required]
        public int RoomHistoryID { get; set; }

        [Required]
        public string CustomerName { get; set; } = string.Empty;
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
