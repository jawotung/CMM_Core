using System;
using System.Collections.Generic;

namespace WebAPI;

public partial class CmmLineOfBusiness
{
    public int LineOfBussId { get; set; }

    public string? LineName { get; set; }

    public string? Description { get; set; }

    public bool? IsActive { get; set; }
}
