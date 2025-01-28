using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Models.Structs
{
    public struct Officer
    {
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string OfficerTypeID { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string DeactivateBy { get; set; }
        public string DeactivationDate { get; set; }
    }
}
