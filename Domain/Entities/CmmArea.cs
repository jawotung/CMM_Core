using System;
using System.Collections.Generic;

namespace WebAPI;

public partial class CmmArea
{
    public int AreaId { get; set; }

    public string AreaCode { get; set; } = "";

    public string AreaName { get; set; } = "";

    public bool? IsActive { get; set; }

    public DateOnly? DateCreated { get; set; }

    public string? CreatedBy { get; set; }

    public DateOnly? DeactivateDate { get; set; }

    public string? DeactivateBy { get; set; }
}
