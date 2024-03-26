using System.ComponentModel.DataAnnotations;

namespace FU_House_Finder.Models
{
    public class Commune
    {
        [Key]
        [Required]
        public int CommuneID { get; set; }

        [MaxLength(40)]
        [Required]
        public string CommuneName { get; set; }

        [Required]
        public int DistrictID { get; set; }

        public District District { get; set; } = null!;

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}
