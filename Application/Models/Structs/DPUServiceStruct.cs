using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Models.Structs
{
    public class DPUServiceStruct
    {
        public string ClientName { get; set; }
        public string ClientType { get; set; }
        public string ClientSegment { get; set; }
        public string ClientSubCategory { get; set; }
        public string ClientRank { get; set; }
        public string IndustryClassification { get; set; }
        public string Branch { get; set; }
        public string Area { get; set; }
        public string BusinessOwner { get; set; }
        public DateTime MonthAvailmentTo { get; set; }
        public DateTime MonthAvailmentFrom { get; set; }
        public string PricingOptions { get; set; }
        public bool? ProductStatus { get; set; }
    }
}
