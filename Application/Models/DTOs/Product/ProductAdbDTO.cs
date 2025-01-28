using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs.Product
{
    public class ProductAdbDTO
    {
        public int ProductAdbId { get; set; }

        public int ProductId { get; set; }
        [Required(ErrorMessage = "ADB Name is required.")]
        [MaxLength(150, ErrorMessage = "ADB Name cannot exceed 150 characters.")]
        public string? AdbName { get; set; }
        [Required(ErrorMessage = "ADB Amount is required.")]
        [MaxLength(150, ErrorMessage = "ADB Amount cannot exceed 150 characters.")]
        public string? AdbAmount { get; set; }
    }
}
