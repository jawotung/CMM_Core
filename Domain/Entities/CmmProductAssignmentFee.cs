using System;
using System.Collections.Generic;

namespace WebAPI;

public partial class CmmProductAssignmentFee
{
    public Guid ClientProductAdbfeeId { get; set; }

    public Guid? ProdAvailId { get; set; }

    public int? ProductAdbId { get; set; }

    public int? TierId { get; set; }

    public int? FeeId { get; set; }

    public string? Caption { get; set; }

    public string? Amount { get; set; }

    public int? IsHeader { get; set; }

    public int? RowOrder { get; set; }
}
