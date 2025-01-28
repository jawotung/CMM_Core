using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Models.Structs
{
    public struct Client
    {
        public Guid ClientID { get; set; }
        public bool ReportType { get; set; }
        public string ClientName { get; set; }
        public string ClientType { get; set; }
        public string ClientSegment { get; set; }
        public string ClientCategory { get; set; }
        public decimal RequiredADB { get; set; }
        public string CifNo { get; set; }
        public string LineOfBusiness { get; set; }
        public string OwnedBy { get; set; }
        public string CmOfficer { get; set; }
        public string Branch { get; set; }
        public string Position { get; set; }
        public string Rank { get; set; }
        public string BusOwner { get; set; }
        public string ProductID { get; set; }
        public string subCategory { get; set; }
        public DateTime ProdAvailedAsOf { get; set; }
        public DateTime ProdAvailedFrom { get; set; }
        public int ProdAvailmentStatus { get; set; }
        public int ClientStatus { get; set; }
        public bool LateReported { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int MeetingRequiredADB { get; set; }
        public string DepositCurrency { get; set; }
        public string AccountStatus { get; set; }
        public string Area { get; set; }
    }
}
