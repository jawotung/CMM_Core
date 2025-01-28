using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs.Product
{
    public class ProductFeeDTO
    {
        public int ProductAdbId { get; set; }

        public int? TierId { get; set; }

        public int FeeId { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(150, ErrorMessage = "Name cannot exceed 150 characters.")]
        public string FeeName { get; set; } = "";
        [Required(ErrorMessage = "Amount is required.")]
        [MaxLength(150, ErrorMessage = "Amount cannot exceed 150 characters.")]
        public string FeeAmount { get; set; } = "";
    }
}
