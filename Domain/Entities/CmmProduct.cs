using System;
using System.Collections.Generic;

namespace WebAPI;

public partial class CmmProduct
{
    public int ProductId { get; set; }

    public string ProductCode { get; set; } = "";

    public string ProductName { get; set; } = "";

    public string Description { get; set; } = "";

    public bool? IsActive { get; set; } = false;

    public DateTime? DateCreated { get; set; }

    public bool? HasChannel { get; set; }

    public bool? HasSite { get; set; }
}
