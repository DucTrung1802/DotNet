﻿using System.ComponentModel.DataAnnotations;

namespace FU_House_Finder.Models
{
    public class Commune
    {
        // Attributes for DTO 
        [Key]
        [Required]
        public int CommuneID { get; set; }

        [MaxLength(40)]
        [Required]
        public string CommuneName { get; set; } = string.Empty;
        // =====================================================

        // Hidden Attributes
        [Required]
        public DateTime CreatedDate { get; set; }
        // =====================================================

        // Foreign Key Attributes
        public District District { get; set; } = null!;

        public int DistrictID { get; set; }

        public ICollection<Village> Villages { get; set; } = new List<Village>();
        // =====================================================
    }
}
