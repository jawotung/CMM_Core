using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Models.Structs
{
    public struct ClientReportDetails
    {
        public string ClientName { get; set; }
        public string ClientCIF { get; set; }
        public string ProductCIF { get; set; }
        public string MainBranch { get; set; }
        public string Area { get; set; }
        public string Line { get; set; }
        public string DebitAcct { get; set; }
        public string MonitoringAcct { get; set; }
        public string ProductName { get; set; }
        public decimal RequiredADB { get; set; }
        public decimal MtdAdb { get; set; }
        public decimal ShortFall { get; set; }
        public bool IsProductActive { get; set; }
        public string CollectionMethod { get; set; }
        public string ProductBranch { get; set; }
        public string CustomerType{ get; set; }
        public string SegmentName { get; set; }
        public string CustCategory { get; set; }
        public string SubCategory { get; set; }
        public string ClientRank { get; set; }
        public string IndustryClass { get; set; }
        public string IsCustomerActive { get; set; }
        public string CMOfficer { get; set; }

    }

    public struct ProductCheckStatus
    {
        public string CIFNo { get; set; }
        public string Payroll { get; set; }
        public string Payment { get; set; }
        public string CheckCutting { get; set; }
        public string CheckwriterPlus { get; set; }
        public string PDCWareHousing { get; set; }
        public string BillsPaymentFacility { get; set; }
        public string BancnetBillsPayment { get; set; }
        public string BancNetPOS { get; set; }
        public string BancnetEMerchant { get; set; }
        public string DepositPickup { get; set; }
        public string CIB { get; set; }
        public string eFPS { get; set; }
        public string SweepFacility { get; set; }
        public string ImportPAS5 { get; set; }
        public string ExportPAS5 { get; set; }
        public string PayrollPlus { get; set; }
    }

    public enum ProductMapping
    { 
        CreditingServicePayroll = 1, 
        CreditingServicePayment = 2,
        CheckCuttingService = 3,
        CheckwriterPlus = 4,
        PDCWarehousing  = 6,
        BillsPaymentFacility = 7,
        BancnetBillsPayment = 8,
        BancnetPOS = 9,
        BancnetEMerchant = 10,
        DepositPickupService = 11,
        CIB = 12,
        BIREFPS = 15,
        SweepFacility = 16,
        ImportPAS5 = 17, 
        ExportPAS5 = 18,
        PayrollPlus = 19,
        FloatProduct = 20,
    }

    public struct ProductsSummary
    {
        public string CIFNo { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateAvailed { get; set; }
        public DateTime DateTerminated { get; set; }
        public int ProductID { get; set; }
    }
}

