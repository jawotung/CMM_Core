using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Models.Structs
{
    public class ProductAvail
    {
        public Guid ProductAvailID { get; set; }
        public Guid ClientID { get; set; }
        public int ProductID { get; set; }
        public Guid ClientAcctID { get; set; }
        public decimal RequiredADB { get; set; }
        public string BranchOfficerID { get; set; }
        public string CMOfficerID { get; set; }
        public int IsActive { get; set; }
        public string DateAvailed { get; set; }
        public string DateTerminated { get; set; }
    }
}
