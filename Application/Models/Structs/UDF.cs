using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Models.Structs
{
    public class UDF
    {
        public Guid ProdAvailID { get; set; }
        public string Value { get; set; }
        public Guid UDFItemID { get; set; }
    }
}
