using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.DTOs.Product;

public class ProductDTO
{
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Product Code is required.")]
    [MaxLength(5, ErrorMessage = "Product Code cannot exceed 5 characters.")]
    public string? ProductCode { get; set; } // Maps to varchar(5)

    [Required(ErrorMessage = "Product Name is required.")]
    [MaxLength(75, ErrorMessage = "Product Name cannot exceed 75 characters.")]
    public string? ProductName { get; set; } // Maps to varchar(75)

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? DateCreated { get; set; }

    public bool? HasChannel { get; set; }

    public bool? HasSite { get; set; }
}
