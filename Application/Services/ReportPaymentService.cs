using Application.Contracts.Repositories;
using Application.Models.Helpers;
using Application.Models.Responses;
using Application.Models.Structs;
using System.Data;
using System.Text;

namespace Application.Services
{
    public class ReportPaymentService
    {

        private const string constProductIDPayment = "2";
        private readonly IReportPaymentRepository _repo;
        private readonly CommonClass _common;
        public ReportPaymentService(IReportPaymentRepository repo, CommonClass common)
        {
            _repo = repo;
            _common = common;
        }

        public async Task<ReturnStatusData<ReturnDownload>> GenerateReport(bool Selected)
        {
            ReturnStatusData<ReturnDownload> result = new(new());
            try
            {
                Payment pr = new Payment(); //await _repo.GetPaymentReportValue();
                pr.ProductID = constProductIDPayment;
                string content = string.Empty;
                if (Selected)
                {
                    content = await BuildPaymentDetailedReport(pr);
                }
                else
                {
                    content = await BuildPaymentSummaryReport(pr);
                }

                if (content.Trim().Length > 0)
                {
                    if (Selected)
                    {
                        result.Data.FileName = string.Format("PaymentDetailedReport{0}.csv", Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10));
                    }
                    else
                    {
                        result.Data.FileName = string.Format("PaymentSummaryReport{0}.csv", Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10));
                    }
                    result.Data.DataBase64 = Convert.ToBase64String(_common.GenerateFileCSV(content));

                }
                else
                {
                    result.Message = "No record(s) found.";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        private async Task<string> BuildPaymentDetailedReport(Payment prInfo)
        {

            DataTable dt = await _repo.GetPaymentDetailedReport(prInfo);
            StringBuilder sb = new StringBuilder();

            if (dt.Rows.Count > 0)
            {
                int year, month;
                month = (DateTime.Now.Month - 1 == 0 ? 12 : DateTime.Now.Month - 1);
                year = (month == 12 ? DateTime.Now.Year - 1 : DateTime.Now.Year);

                sb.AppendFormat("BANK OF COMMERCE{0}", Environment.NewLine);
                sb.AppendFormat("CASH MANAGEMENT MONITORING SYSTEM{0}", Environment.NewLine);
                sb.AppendFormat("CREDITING SERVICE FACILITY-PAYMENT-DETAILED{0}", Environment.NewLine);
                sb.AppendFormat("REPORT DATE: AS OF {0}-{1}{2}", _common.GetMonthString(month), year, Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.AppendFormat("{0}{1}", BuildPaymentDetailedHeader(), Environment.NewLine);
                sb = BuildDetailedData(dt, sb);
            }
            return sb.ToString();

        }
        private async Task<string> BuildPaymentSummaryReport(Payment prInfo)
        {

            DataTable dt = await _repo.GetPaymentSummaryReport(prInfo);
            StringBuilder sb = new StringBuilder();

            if (dt.Rows.Count > 0)
            {
                int year, month;
                month = (DateTime.Now.Month - 1 == 0 ? 12 : DateTime.Now.Month - 1);
                year = (month == 12 ? DateTime.Now.Year - 1 : DateTime.Now.Year);

                sb.AppendFormat("BANK OF COMMERCE{0}", Environment.NewLine);
                sb.AppendFormat("CASH MANAGEMENT MONITORING SYSTEM{0}", Environment.NewLine);
                sb.AppendFormat("CREDITING SERVICE FACILITY-PAYMENT-SUMMARY{0}", Environment.NewLine);
                sb.AppendFormat("REPORT DATE: AS OF {0}-{1}{2}", _common.GetMonthString(month), year, Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.AppendFormat("{0}{1}", BuildPaymentSummaryHeader(), Environment.NewLine);
                sb = BuildSummaryData(dt, sb);
            }
            return sb.ToString();

        }
        private string BuildPaymentDetailedHeader()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("CIF No.,Client Name,Type,Segment,SubCategory,Client Rank,Industry Classification,Branch,Area,");
            sb.AppendFormat("Business Owner,CM Officer,ADB Monitoring Account,Debit Account,Depository Branch,Area,");
            sb.AppendFormat("Product Status,Date Terminated,MOA STATUS,Date Availed,FREQUENCY OF PAYROLL PER YEAR (INCLUDING BONUSES),");
            sb.AppendFormat("No. of Employees (CA),No of Employees (SA),Total Employees,ADB per Employee(CA),ADB per Employee(SA),");
            sb.AppendFormat("Pricing Option,ADB Requirement (No. of emp X ADB per emp),Addtl ADB: Fixed ADB Requiremnt,");
            sb.AppendFormat("TOTAL ADB REQUIRED,Credit Fee,Card Fee,Penalty Collection");

            //sb.AppendFormat("{0}{1}", AdbAndYtdHeaders(), Environment.NewLine);
            return sb.ToString();
        }
        private string BuildPaymentSummaryHeader()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("CIF No.,Client Name,Type,Segment,SubCategory,Client Rank,Industry Classification,Branch,Area,");
            sb.AppendFormat("Business Owner,CM Officer,Total Payroll Companies within the CIF,Total Employee within CIF,Total CA Count,");
            sb.AppendFormat("Total SA Count,Total ADB Requirement (CIF Level),");

            //sb.AppendFormat("{0}{1}", AdbAndYtdHeaders(), Environment.NewLine);
            return sb.ToString();
        }
        private StringBuilder BuildDetailedData(DataTable dt, StringBuilder sbData)
        {
            List<string> acctNoList = new List<string>();

            foreach (DataRow row in dt.Rows)
            {
                decimal noEmpCA;
                decimal adbPerEmpCA;
                decimal noEmpSA;
                decimal adbPerEmpSA;
                decimal AddtlAdb;
                decimal adbReq;
                decimal totalADBRequired;
                GetComputationOfColumn(row, out noEmpCA, out adbPerEmpCA, out noEmpSA, out adbPerEmpSA, out AddtlAdb, out adbReq, out totalADBRequired);

                sbData.AppendFormat("{0},", row["CIFNo"].ToString().Replace(",", ""));
                sbData.AppendFormat("{0},", row["ClientName"].ToString().Replace(",", ""));
                sbData.AppendFormat("{0},", row["Type"].ToString().Replace(",", ""));
                sbData.AppendFormat("{0},", row["Segment"].ToString().Replace(",", ""));
                sbData.AppendFormat("{0},", row["SubCategory"].ToString().Replace(",", ""));
                sbData.AppendFormat("{0},", row["ClientRank"].ToString().Replace(",", ""));
                sbData.AppendFormat("{0},", row["IndustryClass"].ToString().Replace(",", ""));
                sbData.AppendFormat("{0},", row["BranchName"].ToString().Replace(",", ""));
                sbData.AppendFormat("{0},", row["AreaName"].ToString().Replace(",", ""));
                sbData.AppendFormat("{0},", row["BusinessOwner"].ToString().Replace(",", ""));
                sbData.AppendFormat("{0},", row["CMOfficer"].ToString().Replace(",", ""));
                sbData.AppendFormat("{0},", row["ADBMonitoringAcc"].ToString().Replace(",", ""));
                sbData.AppendFormat("{0},", row["DebitAccount"].ToString().Replace(",", ""));
                sbData.AppendFormat("{0},", row["DepBranch"].ToString().Replace(",", ""));
                sbData.AppendFormat("{0},", row["area2"].ToString().Replace(",", ""));
                sbData.AppendFormat("{0},", GetProductStatus(row));
                sbData.AppendFormat("{0},", GetDateNotDispMaxDate(row, "DateTerminated"));
                sbData.AppendFormat("{0},", GetMOAStatus(row));
                sbData.AppendFormat("{0},", GetDateDispMaxDate(row, "DateAvailed"));
                sbData.AppendFormat("{0},", row["FreqPayPerYear"].ToString().Replace(",", ""));
                sbData.AppendFormat("{0},", noEmpCA.ToString().Replace(",", ""));
                sbData.AppendFormat("{0},", noEmpSA.ToString().Replace(",", ""));
                sbData.AppendFormat("{0},", row["TotalEmployee"].ToString().Replace(",", ""));
                sbData.AppendFormat("{0},", adbPerEmpCA.ToString().Replace(",", ""));
                sbData.AppendFormat("{0},", adbPerEmpSA.ToString().Replace(",", ""));
                sbData.AppendFormat("{0},", row["PricingOption"].ToString().Replace(",", ""));
                sbData.AppendFormat("{0},", adbReq.ToString().Replace(",", ""));
                sbData.AppendFormat("{0},", AddtlAdb.ToString().Replace(",", ""));
                sbData.AppendFormat("{0},", totalADBRequired.ToString().Replace(",", ""));
                sbData.AppendFormat("{0},", row["CreditFee"].ToString().Replace(",", ""));
                sbData.AppendFormat("{0},", row["CardFee"].ToString().Replace(",", ""));
                sbData.AppendFormat("{0},", row["PenaltyCollection"].ToString().Replace(",", ""));
                sbData.AppendFormat("{0}", Environment.NewLine);

            }

            return sbData;
        }
        private static void GetComputationOfColumn(DataRow row, out decimal noEmpCA, out decimal adbPerEmpCA, out decimal noEmpSA, out decimal adbPerEmpSA, out decimal AddtlAdb, out decimal adbReq, out decimal totalADBRequired)
        {
            noEmpCA = 0;
            adbPerEmpCA = 0;
            noEmpSA = 0;
            adbPerEmpSA = 0;
            AddtlAdb = 0;
            adbReq = 0;
            totalADBRequired = 0;

            noEmpCA = Convert.ToDecimal(row["NOEmpCA"]);
            adbPerEmpCA = Convert.ToDecimal(row["ADBPerEmpCA"]);
            noEmpSA = Convert.ToDecimal(row["NOEmpSA"]);
            adbPerEmpSA = Convert.ToDecimal(row["ADBPerEmpSA"]);
            adbReq = (noEmpCA + noEmpSA) * (adbPerEmpCA + adbPerEmpSA);

            AddtlAdb = Convert.ToDecimal(row["AddtlADB"]);
            totalADBRequired = adbReq + AddtlAdb;
        }
        private string GetProductStatus(DataRow dr)
        {
            string rtr = string.Empty;

            if (dr["ProductStatus"].ToString().ToLower() == "true")
                rtr = "ACTIVE";
            else if (dr["ProductStatus"].ToString().ToLower() == "false")
                rtr = "TERMINATED";
            else
                rtr = dr["ProductStatus"].ToString().Replace(",", "");

            return rtr;
        }
        private string GetDateNotDispMaxDate(DataRow dr, string nameOfColumn)
        {
            string tempDate = string.Empty;
            tempDate = dr[nameOfColumn].ToString();
            if (tempDate.Trim().Length > 0 && !string.IsNullOrEmpty(tempDate))
            {


                if (tempDate.ToString().Substring(0, 10) == DateTime.MaxValue.ToString().Substring(0, 10))
                {
                    tempDate = string.Empty;
                }
                else
                {
                    if (tempDate.Length == 20)
                        tempDate = tempDate.Substring(0, 8);
                    else if (tempDate.Length == 21)
                        tempDate = tempDate.Substring(0, 9);
                    else if (tempDate.Length == 22)
                        tempDate = tempDate.Substring(0, 10);
                }

            }
            return tempDate;
        }
        private string GetDateDispMaxDate(DataRow dr, string nameOfColumn)
        {
            string tempDate = string.Empty;
            tempDate = dr[nameOfColumn].ToString();
            if (tempDate.Trim().Length > 0 && !string.IsNullOrEmpty(tempDate))
            {
                if (tempDate.Length == 20)
                    tempDate = tempDate.Substring(0, 8);
                else if (tempDate.Length == 21)
                    tempDate = tempDate.Substring(0, 9);
                else if (tempDate.Length == 22)
                    tempDate = tempDate.Substring(0, 10);

            }
            return tempDate;
        }
        private static string GetMOAStatus(DataRow row)
        {
            string moaStatus = row["MOAStatus"].ToString().Replace(",", "").ToLower();

            if (moaStatus == "true")
            {
                moaStatus = "Updated";
            }
            else
            {
                moaStatus = "Not Updated";
            }
            return moaStatus;
        }
        private StringBuilder BuildSummaryData(DataTable dt, StringBuilder sbData)
        {
            List<string> cifNoList = new List<string>();
            decimal totalCA = 0;
            decimal totalSA = 0;
            int cnt = 0;
            int ctr = 1;
            decimal empCtr = 0;
            decimal noEmpCA = 0;
            decimal adbPerEmpCA = 0;
            decimal totalADBRequired = 0;
            decimal addtlADB = 0;
            decimal adbPerEmpSA = 0;
            decimal noEmpSA = 0;
            decimal adbReq = 0;
            foreach (DataRow row in dt.Rows)
            {

                if (!cifNoList.Contains(row["CIFNo"].ToString().Replace(",", "")))
                {
                    cifNoList.Add(row["CIFNo"].ToString().Replace(",", ""));
                    cnt = dt.Select("CIFNo = '" + row["CIFNo"] + "'").Count();
                    ResetIncrementalFieldValue(ref totalCA, ref totalSA, ref noEmpCA, ref adbPerEmpCA, ref addtlADB, ref adbPerEmpSA, ref noEmpSA, ref totalADBRequired, ref adbReq);

                    IncrementFieldValue(ref empCtr, ref totalADBRequired, ref totalCA, ref totalSA, ref noEmpCA, ref adbPerEmpCA, ref addtlADB,
                        ref adbPerEmpSA, ref noEmpSA, ref ctr, row, ref adbReq);

                    sbData.AppendFormat("{0},", row["CIFNo"].ToString().Replace(",", ""));
                    sbData.AppendFormat("{0},", row["ClientName"].ToString().Replace(",", ""));
                    sbData.AppendFormat("{0},", row["CustomerType"].ToString().Replace(",", ""));
                    sbData.AppendFormat("{0},", row["SegmentName"].ToString().Replace(",", ""));
                    sbData.AppendFormat("{0},", row["SubCategoryName"].ToString().Replace(",", ""));
                    sbData.AppendFormat("{0},", row["ClientRank"].ToString().Replace(",", ""));
                    sbData.AppendFormat("{0},", row["IndustryClass"].ToString().Replace(",", ""));
                    sbData.AppendFormat("{0},", row["BranchName"].ToString().Replace(",", ""));
                    sbData.AppendFormat("{0},", row["AreaName"].ToString().Replace(",", ""));
                    sbData.AppendFormat("{0},", row["busowner"].ToString().Replace(",", ""));
                    sbData.AppendFormat("{0},", row["CM Officer"].ToString().Replace(",", ""));
                    sbData.AppendFormat("{0},", row["Total Payroll Companies"].ToString().Replace(",", ""));

                    LastFieldsValue(sbData, totalCA, totalSA, cnt, empCtr, totalADBRequired, ref ctr);

                }
                else
                {
                    IncrementFieldValue(ref empCtr, ref totalADBRequired, ref totalCA, ref totalSA, ref noEmpCA, ref adbPerEmpCA, ref addtlADB,
                        ref adbPerEmpSA, ref noEmpSA, ref ctr, row, ref adbReq);
                    LastFieldsValue(sbData, totalCA, totalSA, cnt, empCtr, totalADBRequired, ref ctr);
                }

            }

            return sbData;
        }
        private static void LastFieldsValue(StringBuilder sbData, decimal totalCA, decimal totalSA, int cnt, decimal empCtr, decimal totalADBRequired, ref int ctr)
        {
            if (ctr - 1 == cnt)
            {
                sbData.AppendFormat("{0},", empCtr.ToString().Replace(",", ""));
                sbData.AppendFormat("{0},", totalCA.ToString().Replace(",", ""));
                sbData.AppendFormat("{0},", totalSA.ToString().Replace(",", ""));
                sbData.AppendFormat("{0},", totalADBRequired.ToString().Replace(",", ""));
                sbData.AppendFormat("{0}", Environment.NewLine);
                ctr = 1;

            }

        }
        private static void ResetIncrementalFieldValue(ref decimal totalCA, ref decimal totalSA, ref decimal noEmpCA, ref decimal adbPerEmpCA, ref decimal addtlADB,
            ref decimal adbPerEmpSA, ref decimal noEmpSA, ref decimal totalADBRequired, ref decimal adbReq)
        {
            totalCA = 0;
            totalSA = 0;
            noEmpCA = 0;
            adbPerEmpCA = 0;
            addtlADB = 0;
            adbPerEmpSA = 0;
            noEmpSA = 0;
            totalADBRequired = 0;
            adbReq = 0;
        }
        private static void IncrementFieldValue(ref decimal empCtr, ref decimal totalADBRequired, ref decimal totalCA, ref decimal totalSA, ref decimal noEmpCA,
            ref decimal adbPerEmpCA, ref decimal addtlADB, ref decimal adbPerEmpSA, ref decimal noEmpSA, ref int ctr, DataRow row, ref decimal adbReq)
        {
            decimal tempADBReq = new decimal();

            totalCA += Convert.ToDecimal(row["TotalCA"].ToString().Replace(",", ""));
            totalSA += Convert.ToDecimal(row["TotalSA"].ToString().Replace(",", ""));

            noEmpCA += Convert.ToDecimal(row["NOEmpCA"]);
            adbPerEmpCA += Convert.ToDecimal(row["ADBPerEmpCA"]);
            noEmpSA += Convert.ToDecimal(row["NOEmpSA"]);
            adbPerEmpSA += Convert.ToDecimal(row["ADBPerEmpSA"]);
            addtlADB += Convert.ToDecimal(row["AddtlADB"].ToString().Replace(",", ""));
            tempADBReq = (Convert.ToDecimal(row["NOEmpCA"]) + Convert.ToDecimal(row["NOEmpSA"])) * (Convert.ToDecimal(row["ADBPerEmpCA"]) + Convert.ToDecimal(row["ADBPerEmpSA"]));
            adbReq += tempADBReq;

            totalADBRequired = adbReq + addtlADB;



            empCtr = totalCA + totalSA;
            ctr += 1;
        }
    }
}
