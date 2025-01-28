using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.DTOs.Product;

public class ProductDTO
{
    public int ProductId { get; set; }

    public string? ProductCode { get; set; }

    public string? ProductName { get; set; }

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? DateCreated { get; set; }

    public bool? HasChannel { get; set; }

    public bool? HasSite { get; set; }
}
