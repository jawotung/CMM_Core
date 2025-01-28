using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Models.Structs
{
    public class Channel
    {
        public int ChannelID { get; set; }
        public string ChannelDesc { get; set; }
        public string ChannelCode { get; set; }
        public bool IsActive { get; set; }
        public Guid ProdChannelID { get; set; }
        public Guid ProdAvailID { get; set; }
    }
}
