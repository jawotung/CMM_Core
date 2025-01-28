using System;
using System.Collections.Generic;

namespace WebAPI;

public partial class CmmFee
{
    public int FeeId { get; set; }

    public string? FeeName { get; set; }

    public string? Description { get; set; }

    public bool? IsActive { get; set; }
}
