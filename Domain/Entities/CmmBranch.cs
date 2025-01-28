using System;
using System.Collections.Generic;

namespace WebAPI;

public partial class CmmBranch
{
    public int BranchId { get; set; }

    public string? BranchName { get; set; }

    public string? BranchCode { get; set; }

    public bool? IsActive { get; set; }

    public int? AreaId { get; set; }

    public DateOnly? DateCreated { get; set; }

    public string? CreatedBy { get; set; }

    public DateOnly? DeactivatedDate { get; set; }

    public string? DeactivatedBy { get; set; }
}
