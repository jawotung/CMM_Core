using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI;

public partial class CmmArea
{
    [Key]
    public int AreaId { get; set; }

    public string AreaCode { get; set; } = "";

    public string AreaName { get; set; } = "";

    public bool? IsActive { get; set; }

    public DateTime DateCreated { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime DeactivateDate { get; set; }

    public string? DeactivateBy { get; set; }
}
