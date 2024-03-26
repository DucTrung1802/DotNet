using System.ComponentModel.DataAnnotations;

namespace FU_House_Finder.Models
{
    public class District
    {
        [Key]
        [Required]
        public int DistrictID { get; set; }

        [MaxLength(40)]
        [Required]
        public string DistrictName { get; set; }

        public ICollection<Commune> Communes { get; set; } = new List<Commune>();

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}