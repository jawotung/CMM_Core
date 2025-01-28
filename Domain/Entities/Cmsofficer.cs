using System;
using System.Collections.Generic;

namespace WebAPI;

public partial class Cmsofficer
{
    public string BranchCode { get; set; } = null!;

    public string? BranchName { get; set; }

    public string? Area { get; set; }

    public string? CmofficerAssignment { get; set; }

    public Guid? OfficerId { get; set; }

    public string? BranchId { get; set; }
}
