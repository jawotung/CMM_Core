using System;
using System.Collections.Generic;

namespace WebAPI;

public partial class CmmLastLogin
{
    public string? UserName { get; set; }

    public DateTime? LastLogin { get; set; }
}
