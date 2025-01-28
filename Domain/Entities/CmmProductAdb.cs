using System;
using System.Collections.Generic;

namespace WebAPI;

public partial class CmmProductAdb
{
    public int ProductAdbId { get; set; }

    public int ProductId { get; set; }

    public string? AdbName { get; set; }

    public string? AdbAmount { get; set; }
}
