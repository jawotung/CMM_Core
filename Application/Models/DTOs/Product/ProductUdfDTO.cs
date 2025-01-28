using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs.Product
{
    public class ProductUdfDTO
    {
        public int? ProductId { get; set; }

        public Guid UdfItemId { get; set; }

        [Required(ErrorMessage = "Field Label is required")]
        [StringLength(50, ErrorMessage = "Field Label cannot exceed 50 characters")]
        public string UdfLabel { get; set; } = string.Empty;

        [Required(ErrorMessage = "Data Type is required")]
        [StringLength(20, ErrorMessage = "UdfType cannot exceed 20 characters")]
        public string UdfType { get; set; } = string.Empty;

        public bool? IsMandatory { get; set; }
    }
}
