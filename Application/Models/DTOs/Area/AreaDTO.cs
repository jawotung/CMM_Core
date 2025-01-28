using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.DTOs.Area;

public class AreaDTO
{
    public int AreaId { get; set; }

    [Required(ErrorMessage = "Area Code is required.")]
    [StringLength(5, ErrorMessage = "Area Code must not exceed 5 characters.")]
    public string? AreaCode { get; set; }

    [Required(ErrorMessage = "Area Name is required.")]
    [StringLength(75, ErrorMessage = "Area Name must not exceed 75 characters.")]
    public string? AreaName { get; set; }

    [Required(ErrorMessage = "IsActive status is required.")]
    public bool? IsActive { get; set; }

    public DateOnly? DateCreated { get; set; }

    public string? CreatedBy { get; set; }

    public DateOnly? DeactivateDate { get; set; }

    public string? DeactivateBy { get; set; }
}
