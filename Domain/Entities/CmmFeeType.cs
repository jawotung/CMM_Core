using System;
using System.Collections.Generic;

namespace WebAPI;

public partial class CmmFeeType
{
    public int FeeNo { get; set; }

    public string? FeeId { get; set; }

    public Guid? ClientId { get; set; }

    public string? FixAmount { get; set; }

    public string? Field1 { get; set; }

    public string? Field1Amount { get; set; }

    public string? Field2 { get; set; }

    public string? Field2Amount { get; set; }

    public string? Field3 { get; set; }

    public string? Field3Amount { get; set; }
}
