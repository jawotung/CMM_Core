using System;
using System.Collections.Generic;

namespace WebAPI;

public partial class CmmProductFee
{
    public int ProductAdbId { get; set; }

    public int? TierId { get; set; }

    public int FeeId { get; set; }

    public string? FeeName { get; set; }

    public string? FeeAmount { get; set; }
}
