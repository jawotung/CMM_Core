using System;
using System.Collections.Generic;

namespace WebAPI;

public partial class CmmPickupSite
{
    public Guid? SiteId { get; set; }

    public string? SvcUnit { get; set; }

    public string? Address { get; set; }

    public int? Frequency { get; set; }

    public string? FreqUnit { get; set; }

    public DateOnly? DateAvailed { get; set; }

    public decimal? Adbreq { get; set; }

    public bool? IsActive { get; set; }

    public Guid? ProdAvailId { get; set; }

    public int? ServicingTime { get; set; }

    public string? FreqSchedule { get; set; }

    public virtual CmmProductAssignment? ProdAvail { get; set; }
}
