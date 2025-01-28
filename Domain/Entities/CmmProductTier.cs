using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI;

public partial class CmmProductTier
{
    public int ProductAdbId { get; set; }

    public string AdbAmount { get; set; } = "";

    public int TierId { get; set; }
    public string TierName { get; set; } = "";

    public string TierDesc { get; set; } = "";
}
