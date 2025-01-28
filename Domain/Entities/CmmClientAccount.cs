using System;
using System.Collections.Generic;

namespace WebAPI;

public partial class CmmClientAccount
{
    public Guid ClientAcctId { get; set; }

    public Guid? ClientId { get; set; }

    public string? Cifno { get; set; }

    public string? AccountNo { get; set; }

    public DateTime? DateCreated { get; set; }

    public string? CreatedBy { get; set; }

    public string? VerifiedBy { get; set; }

    public DateTime? DateVerified { get; set; }

    public bool? IsEnrolled { get; set; }
}
