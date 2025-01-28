using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs.Product
{
    public class ProductTierDTO
    {
        public int ProductAdbId { get; set; }

        public string? AdbAmount { get; set; }

        public int TierId { get; set; }

        [Required(ErrorMessage = "Tier Name is required.")]
        [MaxLength(150, ErrorMessage = "Tier Name cannot exceed 150 characters.")]

        public string? TierName { get; set; }
        [Required(ErrorMessage = "Tier Desc is required.")]
        [MaxLength(150, ErrorMessage = "Tier Desc cannot exceed 150 characters.")]

        public string? TierDesc { get; set; }
    }
}
