using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Models.Structs
{
    public class PickupSite
    {
        public Guid ProdAvailID { get; set; }
        public string ServicingUnit { get; set; }
        public string Address { get; set; }
        public int Frequency { get; set; }
        public string FrequencyUnit { get; set; }
        public string Schedule { get; set; }
        public string DateAvailed { get; set; }
        public decimal ADBRequirement { get; set; }
        public int ServicingTime { get; set; }
        public char Status { get; set; }
        public Guid SiteID { get; set; }
    }
}
