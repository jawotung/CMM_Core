using System;
using System.Collections.Generic;

namespace WebAPI;

public partial class CmmAdbentry
{
    public Guid? Id { get; set; }

    public string? AccountNo { get; set; }

    public DateOnly? AsOfDate { get; set; }

    public decimal? AverageBalance { get; set; }
}
