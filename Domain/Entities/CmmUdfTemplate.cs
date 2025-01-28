using System;
using System.Collections.Generic;

namespace WebAPI;

public partial class CmmUdfTemplate
{
    public int? ProductId { get; set; }

    public Guid UdfItemId { get; set; }

    public string? UdfLabel { get; set; }

    public string? UdfType { get; set; }

    public bool? IsMandatory { get; set; }
}
