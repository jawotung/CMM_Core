using System;
using System.Collections.Generic;

namespace WebAPI;

public partial class CmmError
{
    public string? ErrorMessage { get; set; }

    public string? ErrorSource { get; set; }

    public DateOnly? LogDate { get; set; }
}
