using System;
using System.Collections.Generic;

namespace WebAPI;

public partial class CmmUdfTemplate
{
    public int ProductId { get; set; }

    public Guid UdfItemId { get; set; }

    public string UdfLabel { get; set; } = string.Empty;

    public string UdfType { get; set; } = string.Empty;

    public bool? IsMandatory { get; set; }
}
