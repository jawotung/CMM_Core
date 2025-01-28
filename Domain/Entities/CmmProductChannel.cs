using System;
using System.Collections.Generic;

namespace WebAPI;

public partial class CmmProductChannel
{
    public Guid ProductChannelId { get; set; }

    public Guid? ProductAvailId { get; set; }

    public int? ChannelId { get; set; }

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public DateOnly? DateAvailed { get; set; }

    public DateOnly? DateTerminated { get; set; }

    public virtual CmmChannel? Channel { get; set; }

    public virtual CmmProductAssignment? ProductAvail { get; set; }
}
