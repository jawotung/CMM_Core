using System;
using System.Collections.Generic;

namespace WebAPI;

public partial class CmmCollectionDetail
{
    public Guid CollectionDetailId { get; set; }

    public Guid? ProdAvailId { get; set; }

    public int? CollectionMethodId { get; set; }

    public int? DayOfMonth { get; set; }

    public string? EmailOrHardCopy { get; set; }

    public string? Name { get; set; }

    public string? Position { get; set; }

    public string? Department { get; set; }

    public string? Email { get; set; }

    public string? OtherInstructions { get; set; }

    public string? Address { get; set; }

    public bool? HasEmail { get; set; }

    public bool? HasHardCopy { get; set; }
}
