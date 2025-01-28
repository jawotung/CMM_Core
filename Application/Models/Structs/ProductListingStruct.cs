using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Models.Structs
{
    public class ProductListingStruct
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int Status { get; set; }
        public string ProductList { get; set; }
    }
}
