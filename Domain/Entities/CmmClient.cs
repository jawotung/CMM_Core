using System;
using System.Collections.Generic;

namespace WebAPI;

public partial class CmmClient
{
    public Guid ClientId { get; set; }

    public string? ClientName { get; set; }

    public string? Description { get; set; }

    public int? TypeId { get; set; }

    public int? SegmentId { get; set; }

    public int? LineOfBussId { get; set; }

    public int? CategoryId { get; set; }

    public Guid? AcctOwnerId { get; set; }

    public Guid? CmofficerId { get; set; }

    public decimal? RequiredAdb { get; set; }

    public string? Cifno { get; set; }

    public int? BranchId { get; set; }

    public int? ClientRank { get; set; }

    public int? BusOwner { get; set; }

    public int? SubCategory { get; set; }

    public string? Fremark { get; set; }

    public string? Sremark { get; set; }

    public bool? IsDeleted { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DateDeleted { get; set; }
}
