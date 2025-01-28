using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Models.Structs
{
    public struct FeesStruct
    {
        public string FeeID { get; set; }
        public Guid ClientID { get; set; }
        public string FixAmount { get; set; }
        public string Field1 { get; set; }
        public string Field1Amount { get; set; }
        public string Field2 { get; set; }
        public string Field2Amount { get; set; }
        public string Field3 { get; set; }
        public string Field3Amount { get; set; }
    }
}
