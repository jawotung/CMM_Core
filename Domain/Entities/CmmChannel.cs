using System;
using System.Collections.Generic;

namespace WebAPI;

public partial class CmmChannel
{
    public int ChannelId { get; set; }

    public string? ChannelName { get; set; }

    public string? Description { get; set; }

    public DateTime? DateCreated { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<CmmProductChannel> CmmProductChannels { get; set; } = new List<CmmProductChannel>();
}
