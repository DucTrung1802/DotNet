using System.ComponentModel.DataAnnotations;

namespace FU_House_Finder.Models
{
    public class Room
    {
        // Attributes for DTO
        [Key]
        [Required]
        public int RoomID { get; set; }

        [MaxLength(100)]
        [Required]
        public string RoomName { get; set; } = string.Empty;

        [Required]
        public double Price { get; set; }

        [MaxLength(400)]
        [Required]
        public string Information { get; set; } = string.Empty;

        [Required]
        public double Area { get; set; }

        [Required]
        public bool Airon { get; set; }

        [Required]
        public int MaxAmountOfPeople { get; set; }

        [Required]
        public int CurrentAmountOfPeople { get; set; }

        [Required]
        public int BuildingNumber { get; set; }

        [Required]
        public int FloorNumber { get; set; }
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
