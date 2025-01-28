using System;
using System.Collections.Generic;

namespace WebAPI;

public partial class CmmProductAssignment
{
    public Guid ProdAvailId { get; set; }

    public Guid? ClientId { get; set; }

    public int? ProductId { get; set; }

    public DateOnly? DateAvailed { get; set; }

    public DateOnly? DateTerminated { get; set; }

    public string? ReferredBy { get; set; }

    public string? Cmofficer { get; set; }

    public int? OfficerTypeId { get; set; }

    public Guid? ClientAcctId { get; set; }

    public Guid? DebitAcctId { get; set; }

    public string? MonitorAcct { get; set; }

    public string? DebitAcct { get; set; }

    public string? DepositoryBranch { get; set; }

    public string? Area { get; set; }

    public bool? IsActive { get; set; }

    public decimal? Adbrequired { get; set; }

    public DateOnly? DateInactive { get; set; }

    public string? InactiveBy { get; set; }

    public bool? IsMoaOnFile { get; set; }

    public bool? IsMoaNotUpdated { get; set; }

    public virtual ICollection<CmmProductChannel> CmmProductChannels { get; set; } = new List<CmmProductChannel>();
}
