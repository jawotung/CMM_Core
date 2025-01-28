using Application.Contracts.Repositories;
using Application.Models.DTOs.Product;
using Application.Models.Helpers;
using Application.Models.Responses;
using Application.Models.Structs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{

    public class ReportADBService
    {
        private readonly IReportADBRepository _repo;
        private readonly CommonClass _common;
        public ReportADBService(IReportADBRepository repo, CommonClass common)
        {
            _repo = repo;
            _common = common;
        }
        public async Task<ReturnStatusData<ReturnDownload>> GenerateReport(Client clientInfo)
        {
            ReturnStatusData<ReturnDownload> result = new(new());
            try
            {
                if (!clientInfo.ReportType)
                {
                    result.Message = "Temporarily unavailable.";
                    return result;
                }

                if (ValidateDates(clientInfo))
                {
                    string content = string.Empty;

                    if (clientInfo.ReportType)
                    {
                        content = await BuildADBReportSummary(clientInfo);
                    }
                    else
                    {
                        content = await BuildADBReport(clientInfo);
                    }

                    if (content.Trim().Length > 0)
                    {
                        result.Data.FileName = string.Format("ADBReportSummary{0}.csv", Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10));
                        result.Data.DataBase64 = Convert.ToBase64String(_common.GenerateFileCSV(content));
                    }
                    else
                    {
                        result.Message = "No record(s) found.";
                    }
                }
                else
                {
                    result.Message = "Some data needed for the date range provided is not available. If you really need to access the report for this date range, please contact ITSD.";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        private bool ValidateDates(Client clientInfo)
        {
            bool isValid = true;

            if (clientInfo.ProdAvailedAsOf != DateTime.MinValue)
            {
                if (clientInfo.ProdAvailedAsOf.Year < DateTime.Now.Year && DateTime.Now.Month > 1)
                {
                    isValid = false;
                }
            }

            if (clientInfo.ProdAvailedFrom != DateTime.MinValue)
            {
                if (clientInfo.ProdAvailedFrom.Year < DateTime.Now.Year && DateTime.Now.Month > 1)
                {
                    isValid = false;
                }
            }

            return isValid;
        }
        public async Task<string> BuildADBReport(Client clientInfo)
        {

            DataTable dt = await _repo.SelectYTD(clientInfo);
            StringBuilder sb = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                int year, month;
                month = (DateTime.Now.Month - 1 == 0 ? 12 : DateTime.Now.Month - 1);
                year = (month == 12 ? DateTime.Now.Year - 1 : DateTime.Now.Year);

                sb.AppendFormat("BANK OF COMMERCE{0}", Environment.NewLine);
                sb.AppendFormat("CASH MANAGEMENT MONITORING SYSTEM{0}", Environment.NewLine);
                sb.AppendFormat("CASH MANAGEMENT - ADB REPORT (ACCOUNT LEVEL){0}", Environment.NewLine);
                sb.AppendFormat("REPORT MONTH DATE: AS OF {0}-{1}{2}",_common.GetMonthString(month), year, Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.AppendFormat(",,,,,,,,,,,,,MTD ADB(OF ACCOUNT),,,,,,,,,,,,YTD ADB OF ACCOUNT{0}", Environment.NewLine);
                sb.AppendFormat("{0}{1}", BuildHeader(), Environment.NewLine);
                sb = BuildData(dt, sb);
            }
            return sb.ToString();
        }
        public async Task<string> BuildADBReportSummary(Client clientInfo)
        {

            DataTable dt = await _repo.SelectYTDSummary(clientInfo);
            StringBuilder sb = new StringBuilder();

            if (dt.Rows.Count > 0)
            {

                int year, month;
                month = (DateTime.Now.Month - 1 == 0 ? 12 : DateTime.Now.Month - 1);
                year = (month == 12 ? DateTime.Now.Year - 1 : DateTime.Now.Year);

                sb.AppendFormat("BANK OF COMMERCE{0}", Environment.NewLine);
                sb.AppendFormat("CASH MANAGEMENT MONITORING SYSTEM{0}", Environment.NewLine);
                sb.AppendFormat("CASH MANAGEMENT - ADB REPORT (CIF LEVEL)-SUMMARY{0}", Environment.NewLine);
                sb.AppendFormat("REPORT MONTH DATE: AS OF {0}-{1}{2}", _common.GetMonthString(month), year, Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);

                sb.AppendFormat("{0}{1}", BuildHeaderSummary(), Environment.NewLine);
                sb = await BuildDataSummary(dt, sb, clientInfo);
            }
            return sb.ToString();




        }
        private async Task<StringBuilder> BuildDataSummary(DataTable dt, StringBuilder sbData, Client clientInfo)
        {
            DataView view = new DataView(dt);
            DataTable distinct = view.ToTable(true, "CIFNo");
            List<string> cifList = new List<string>();
            DataTable dtProductList = await GetProductsAvailedSummary(clientInfo);

            DataTable dtProductName = await _repo.SelectAllProduct();

            foreach (DataRow row in distinct.Rows)
            {
                //CIF
                foreach (DataRow item in dt.Select("CIFNo = '" + row["CIFNo"].ToString() + "'"))
                {
                    DataTable dtAccounts = await _repo.GetAllAcounts(row["CIFNo"].ToString());
                    int adbActiveAcctCA = 0;
                    int adbActiveAcctSA = 0;
                    int adbInActiveAcctCASA = 0;
                    decimal totalADBRequired = 0;
                    string mtd = string.Empty;
                    string ytd = string.Empty;
                    //Account
                    foreach (DataRow account in dtAccounts.Rows)
                    {
                        if (account["Status"].ToString().Trim().ToUpper() == "ACTIVE" && account["AccountNo"].ToString().Trim().ToUpper().Substring(3, 2) == "00")
                        {
                            adbActiveAcctCA++;
                        }

                        if (account["Status"].ToString().Trim().ToUpper() == "ACTIVE" && account["AccountNo"].ToString().Trim().ToUpper().Substring(3, 2) == "20")
                        {
                            adbActiveAcctSA++;
                        }

                        if ((account["Status"].ToString().Trim().ToUpper() == "CLOSED" && account["AccountNo"].ToString().Trim().ToUpper().Substring(3, 2) == "00") ||
                            (account["Status"].ToString().Trim().ToUpper() == "CLOSED" && account["AccountNo"].ToString().Trim().ToUpper().Substring(3, 2) == "20"))
                        {
                            adbInActiveAcctCASA++;
                        }
                    }
                    if (item["ADBRequired"].ToString() != string.Empty)
                    {
                        totalADBRequired += int.Parse(item["ADBRequired"].ToString());
                    }

                    if (!cifList.Contains(item["CIFNo"].ToString().Replace(",", "")))
                    {
                        mtd = GetADBSummaryMTD(dtAccounts);
                        List<decimal> adbYTDList = GetAggregateYTD(dtAccounts);
                        ytd = BuildYTDDataSummary(adbYTDList);
                        cifList.Add(item["CIFNo"].ToString().Replace(",", ""));
                        sbData.AppendFormat("{0},", item["CIFNo"].ToString().Replace(",", ""));
                        sbData.AppendFormat("{0},", item["ClientName"].ToString().Replace(",", ""));
                        sbData.AppendFormat("{0},", item["BranchName"].ToString().Replace(",", ""));
                        sbData.AppendFormat("{0},", item["AreaName"].ToString().Replace(",", ""));
                        sbData.AppendFormat("{0},", item["BusOwner"].ToString().Replace(",", ""));
                        sbData.AppendFormat("{0},", item["SegmentName"].ToString().Replace(",", ""));
                        sbData.AppendFormat("{0},", item["CustCategory"].ToString().Replace(",", ""));
                        sbData.AppendFormat("{0},", item["SubCategoryName"].ToString().Replace(",", ""));
                        sbData.AppendFormat("{0},", item["ClientRank"].ToString().Replace(",", ""));
                        sbData.AppendFormat("{0},", item["IndustryClass"].ToString().Replace(",", ""));
                        sbData.AppendFormat("{0},", adbActiveAcctCA.ToString());
                        sbData.AppendFormat("{0},", adbActiveAcctSA.ToString());
                        sbData.AppendFormat("{0},", adbInActiveAcctCASA.ToString());
                        sbData.AppendFormat("{0},", Convert.ToBoolean(item["CashCustomerTagging"]) ? "INACTIVE" : "ACTIVE");
                        if (adbActiveAcctCA + adbActiveAcctSA > 0)
                        {
                            sbData.AppendFormat("{0},", "ACTIVE PHP CASA");
                        }
                        else
                        {
                            sbData.AppendFormat("{0},", "NO PHP CASA");
                        }
                        sbData.AppendFormat("{0},", totalADBRequired.ToString());
                        sbData.AppendFormat("{0}", GetProductsAvailedSummaryByCIF(dt.Select("CIFNo = '" + item["CIFNo"].ToString() + "'"), dtProductName));

                        sbData.AppendFormat("{0}", mtd);
                        sbData.AppendFormat("{0}", ytd);
                        sbData.AppendFormat("{0}", Environment.NewLine);
                    }
                }
            }


            return sbData;
        }
        private string GetProductsAvailedSummaryByCIF(DataRow[] dataRowList, DataTable dtProductName)
        {
            List<ProductsSummary> summary = new List<ProductsSummary>();

            foreach (DataRow row in dataRowList)
            {
                ProductsSummary item = new ProductsSummary();
                item.CIFNo = row["CIFNo"].ToString();
                item.ProductID = Convert.ToInt32(row["ProductID"]);
                Boolean isActive = Convert.ToBoolean(row["IsActive"]);
                if (isActive == true)
                {
                    item.DateAvailed = Convert.ToDateTime(row["DateAvailed"]);
                }
                else
                {
                    item.DateTerminated = Convert.ToDateTime(row["DateTerminated"]);
                }

                item.IsActive = isActive;

                summary.Add(item);
            }


            string summaryString = FormatProductSummary(summary, dtProductName);
            return summaryString;
        }
        private string FormatProductSummary(List<ProductsSummary> summary, DataTable dtProductName)
        {
            string rtr = string.Empty;

            foreach (DataRow item in dtProductName.Rows)
            {
                int productId = Convert.ToInt32(item["ProductID"]);

                var active = (from u in summary
                              where u.IsActive == true && u.ProductID == productId
                              select new ProductsSummary
                              {
                                  CIFNo = u.CIFNo,
                                  DateAvailed = u.DateAvailed,
                                  DateTerminated = u.DateTerminated,
                                  IsActive = u.IsActive,
                                  ProductID = u.ProductID
                              }).ToList().OrderBy(a => a.DateAvailed);

                var terminate = (from u in summary
                                 where u.IsActive == false && u.ProductID == productId
                                 select new ProductsSummary
                                 {
                                     CIFNo = u.CIFNo,
                                     DateAvailed = u.DateAvailed,
                                     DateTerminated = u.DateTerminated,
                                     IsActive = u.IsActive,
                                     ProductID = u.ProductID
                                 }).ToList().OrderBy(a => a.DateTerminated);

                var activeProduct = active.FirstOrDefault();
                var terminateProduct = terminate.LastOrDefault();

                if (active.Count() > 0 && terminate.Count() > 0)
                {
                    if (active.LastOrDefault().DateAvailed <= terminate.LastOrDefault().DateTerminated)
                    {
                        rtr += string.Format("TR-{0}-{1},", terminateProduct.DateTerminated.Year.ToString(), terminateProduct.DateTerminated.Month.ToString());
                    }
                    else
                    {
                        rtr += string.Format("AC-{0}-{1},", activeProduct.DateAvailed.Year.ToString(), activeProduct.DateAvailed.Month.ToString());
                    }
                }
                else if (active.Count() > 0)
                {
                    rtr += string.Format("AC-{0}-{1},", activeProduct.DateAvailed.Year.ToString(), activeProduct.DateAvailed.Month.ToString());
                }
                else if (terminate.Count() > 0)
                {
                    rtr += string.Format("TR-{0}-{1},", terminateProduct.DateTerminated.Year.ToString(), terminateProduct.DateTerminated.Month.ToString());
                }
                else
                {
                    rtr += string.Format("{0},", "");
                }

            }



            return rtr;
        }
        private async Task<DataTable> GetProductsAvailedSummary(Client clientInfo)
        {
            DataTable dtResult = await _repo.GetProductListing(clientInfo);


            return dtResult;
        }
        private List<decimal> GetAggregateYTD(DataTable dtAccounts)
        {
            StringBuilder sbData = new StringBuilder();
            decimal adbJan = new decimal();
            decimal adbFeb = new decimal();
            decimal adbMar = new decimal();
            decimal adbApr = new decimal();
            decimal adbMay = new decimal();
            decimal adbJun = new decimal();
            decimal adbJul = new decimal();
            decimal adbAug = new decimal();
            decimal adbSep = new decimal();
            decimal adbOct = new decimal();
            decimal adbNov = new decimal();
            decimal adbDec = new decimal();

            foreach (DataRow account in dtAccounts.Rows)
            {
                if (account["Status"].ToString().Trim() == "ACTIVE" &&
                    (account["AccountNo"].ToString().Substring(3, 2).Trim() == "20" || account["AccountNo"].ToString().Substring(3, 2).Trim() == "00"))
                {

                    adbJan += Convert.ToDecimal(account["ADBJAN"].ToString().Replace(",", "").Trim());
                    adbFeb += Convert.ToDecimal(account["ADBFEB"].ToString().Replace(",", "").Trim());
                    adbMar += Convert.ToDecimal(account["ADBMAR"].ToString().Replace(",", "").Trim());
                    adbApr += Convert.ToDecimal(account["ADBAPR"].ToString().Replace(",", "").Trim());
                    adbMay += Convert.ToDecimal(account["ADBMAY"].ToString().Replace(",", "").Trim());
                    adbJun += Convert.ToDecimal(account["ADBJUN"].ToString().Replace(",", "").Trim());
                    adbJul += Convert.ToDecimal(account["ADBJUL"].ToString().Replace(",", "").Trim());
                    adbAug += Convert.ToDecimal(account["ADBAUG"].ToString().Replace(",", "").Trim());
                    adbSep += Convert.ToDecimal(account["ADBSEP"].ToString().Replace(",", "").Trim());
                    adbOct += Convert.ToDecimal(account["ADBOCT"].ToString().Replace(",", "").Trim());
                    adbNov += Convert.ToDecimal(account["ADBNOV"].ToString().Replace(",", "").Trim());
                    adbDec += Convert.ToDecimal(account["ADBDEC"].ToString().Replace(",", "").Trim());
                }
            }

            List<decimal> adbList = new List<decimal>();
            adbList.Add(0);
            adbList.Add(adbJan);
            adbList.Add(adbFeb);
            adbList.Add(adbMar);
            adbList.Add(adbApr);
            adbList.Add(adbMay);
            adbList.Add(adbJun);
            adbList.Add(adbJul);
            adbList.Add(adbAug);
            adbList.Add(adbSep);
            adbList.Add(adbOct);
            adbList.Add(adbNov);
            adbList.Add(adbDec);

            return adbList;
        }
        private string GetADBSummaryMTD(DataTable dtAccounts)
        {
            StringBuilder sbData = new StringBuilder();
            decimal adbJan = new decimal();
            decimal adbFeb = new decimal();
            decimal adbMar = new decimal();
            decimal adbApr = new decimal();
            decimal adbMay = new decimal();
            decimal adbJun = new decimal();
            decimal adbJul = new decimal();
            decimal adbAug = new decimal();
            decimal adbSep = new decimal();
            decimal adbOct = new decimal();
            decimal adbNov = new decimal();
            decimal adbDec = new decimal();

            foreach (DataRow account in dtAccounts.Rows)
            {
                if (account["Status"].ToString().Trim() == "ACTIVE" &&
                    (account["AccountNo"].ToString().Substring(3, 2).Trim() == "20" || account["AccountNo"].ToString().Substring(3, 2).Trim() == "00"))
                {
                    adbJan += Convert.ToDecimal(ComputeADBAmount(account["ADBJAN"].ToString().Replace(",", "").Trim(), 1));
                    adbFeb += Convert.ToDecimal(ComputeADBAmount(account["ADBFEB"].ToString().Replace(",", "").Trim(), 2));
                    adbMar += Convert.ToDecimal(ComputeADBAmount(account["ADBMAR"].ToString().Replace(",", "").Trim(), 3));
                    adbApr += Convert.ToDecimal(ComputeADBAmount(account["ADBAPR"].ToString().Replace(",", "").Trim(), 4));
                    adbMay += Convert.ToDecimal(ComputeADBAmount(account["ADBMAY"].ToString().Replace(",", "").Trim(), 5));
                    adbJun += Convert.ToDecimal(ComputeADBAmount(account["ADBJUN"].ToString().Replace(",", "").Trim(), 6));
                    adbJul += Convert.ToDecimal(ComputeADBAmount(account["ADBJUL"].ToString().Replace(",", "").Trim(), 7));
                    adbAug += Convert.ToDecimal(ComputeADBAmount(account["ADBAUG"].ToString().Replace(",", "").Trim(), 8));
                    adbSep += Convert.ToDecimal(ComputeADBAmount(account["ADBSEP"].ToString().Replace(",", "").Trim(), 9));
                    adbOct += Convert.ToDecimal(ComputeADBAmount(account["ADBOCT"].ToString().Replace(",", "").Trim(), 10));
                    adbNov += Convert.ToDecimal(ComputeADBAmount(account["ADBNOV"].ToString().Replace(",", "").Trim(), 11));
                    adbDec += Convert.ToDecimal(ComputeADBAmount(account["ADBDEC"].ToString().Replace(",", "").Trim(), 12));

                }
            }

            sbData.AppendFormat("{0},", adbJan);
            sbData.AppendFormat("{0},", adbFeb);
            sbData.AppendFormat("{0},", adbMar);
            sbData.AppendFormat("{0},", adbApr);
            sbData.AppendFormat("{0},", adbMay);
            sbData.AppendFormat("{0},", adbJun);
            sbData.AppendFormat("{0},", adbJul);
            sbData.AppendFormat("{0},", adbAug);
            sbData.AppendFormat("{0},", adbSep);
            sbData.AppendFormat("{0},", adbOct);
            sbData.AppendFormat("{0},", adbNov);
            sbData.AppendFormat("{0},", adbDec);

            return sbData.ToString();
        }
        private static void YTDSummaryValueCIFLevel(StringBuilder sbData, string ytdValueList)
        {
            var splitedRN = ytdValueList.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < 12; i++)
            {
                double totalSplitedValue = 0;
                for (int y = 0; y < splitedRN.Count(); y++)
                {
                    totalSplitedValue += (splitedRN[y].Split(',')[i].Trim() == string.Empty ? 0 : Convert.ToDouble(splitedRN[y].Split(',')[i]));

                }
                sbData.AppendFormat("{0},", totalSplitedValue.ToString().Replace(",", ""));
            }
        }
        private StringBuilder BuildData(DataTable dt, StringBuilder sbData)
        {
            List<string> acctNoList = new List<string>();

            foreach (DataRow row in dt.Rows)
            {
                string accntNo = row["MonitorAcct"].ToString().Replace(",", "").PadLeft(12, '0');


                if (accntNo.Substring(3, 2) == "20" || accntNo.Substring(3, 2) == "00")
                {
                    if (!acctNoList.Contains(accntNo))
                    {
                        acctNoList.Add(accntNo);
                        sbData.AppendFormat("{0},", row["ClientCIF"].ToString().Replace(",", "")); //CIFNo
                        sbData.AppendFormat("{0},", row["ClientName"].ToString().Replace(",", "")); //ClientName
                        sbData.AppendFormat("{0},", row["ProductBranch"].ToString().Replace(",", "")); //BranchCode
                        sbData.AppendFormat("{0},", row["BranchCode"].ToString().Replace(",", "")); //BranchName
                        sbData.AppendFormat("{0},", row["AreaName"].ToString().Replace(",", "")); //AreaName
                        sbData.AppendFormat("{0},", row["SegmentName"].ToString().Replace(",", "")); //CustomerSegment
                        sbData.AppendFormat("{0},", row["CustCategory"].ToString().Replace(",", "")); //Category
                        sbData.AppendFormat("{0},", row["SubCategoryName"].ToString().Replace(",", "")); //SubCategory
                        sbData.AppendFormat("{0},", row["ClientRank"].ToString().Replace(",", "")); //ClientRank
                        sbData.AppendFormat("{0},", row["IndustryClass"].ToString().Replace(",", "")); //IndustryClass
                        sbData.AppendFormat("{0},", row["MonitorAcct"].ToString().Replace(",", "")); //CASA Acct No
                        sbData.AppendFormat("{0},", row["AcctType"].ToString().Replace(",", "")); //Acct Type
                        sbData.AppendFormat("{0},", row["Status"].ToString().Replace(",", "").Trim()); //Acct Status
                        sbData.AppendFormat("{0},", ComputeADBAmount(row["ADBJAN"].ToString().Replace(",", "").Trim(), 1)); //Acct Status
                        sbData.AppendFormat("{0},", ComputeADBAmount(row["ADBFEB"].ToString().Replace(",", "").Trim(), 2)); //Acct Status
                        sbData.AppendFormat("{0},", ComputeADBAmount(row["ADBMAR"].ToString().Replace(",", "").Trim(), 3)); //Acct Status
                        sbData.AppendFormat("{0},", ComputeADBAmount(row["ADBAPR"].ToString().Replace(",", "").Trim(), 4)); //Acct Status
                        sbData.AppendFormat("{0},", ComputeADBAmount(row["ADBMAY"].ToString().Replace(",", "").Trim(), 5)); //Acct Status
                        sbData.AppendFormat("{0},", ComputeADBAmount(row["ADBJUN"].ToString().Replace(",", "").Trim(), 6)); //Acct Status
                        sbData.AppendFormat("{0},", ComputeADBAmount(row["ADBJUL"].ToString().Replace(",", "").Trim(), 7)); //Acct Status
                        sbData.AppendFormat("{0},", ComputeADBAmount(row["ADBAUG"].ToString().Replace(",", "").Trim(), 8)); //Acct Status
                        sbData.AppendFormat("{0},", ComputeADBAmount(row["ADBSEP"].ToString().Replace(",", "").Trim(), 9)); //Acct Status
                        sbData.AppendFormat("{0},", ComputeADBAmount(row["ADBOCT"].ToString().Replace(",", "").Trim(), 10)); //Acct Status
                        sbData.AppendFormat("{0},", ComputeADBAmount(row["ADBNOV"].ToString().Replace(",", "").Trim(), 11)); //Acct Status
                        sbData.AppendFormat("{0},", ComputeADBAmount(row["ADBDEC"].ToString().Replace(",", "").Trim(), 12)); //Acct Status
                        sbData.AppendFormat(BuildYTDData(row));


                    }
                }
                else
                {
                    sbData.ToString();
                }
            }

            return sbData;
        }
        private string ComputeADBAmount(string strAmount, int month)
        {
            int days = 30;
            int year = DateTime.Now.Year;
            decimal amount = decimal.Parse(strAmount);

            //if Feb
            if (month == 2)
            {
                //check current month
                if (DateTime.Now.Month <= 2)
                {
                    year -= 1;
                }

            }

            days = DateTime.DaysInMonth(year, month);
            amount = amount / days;

            return amount.ToString();
        }
        private string BuildYTDData(DataRow row)
        {
            StringBuilder strYTDData = new StringBuilder();
            int daysInMonth = 0;
            double adb = 0;

            for (int i = 1; i <= 12; i++)
            {
                if (Convert.ToInt32(GetApplicableYear(i)) == DateTime.Now.Year || DateTime.Now.Month == 1)
                {
                    daysInMonth = 0;
                    adb = 0;
                    for (int j = 1; j <= i; j++)
                    {
                        daysInMonth += DateTime.DaysInMonth(Convert.ToInt32(GetApplicableYear(i)), j);
                        adb += Convert.ToDouble(row[string.Format("ADB{0}", _common.GetMonthString(j))]);
                    }
                    strYTDData.Append(adb / daysInMonth);
                }
                strYTDData.Append(",");
            }
            strYTDData.Append(Environment.NewLine);

            return strYTDData.ToString();
        }
        private string BuildYTDDataSummary(List<decimal> adbYTDList)
        {
            StringBuilder strYTDData = new StringBuilder();
            int daysInMonth = 0;
            decimal adb = 0;

            for (int i = 1; i <= 12; i++)
            {
                if (Convert.ToInt32(GetApplicableYear(i)) == DateTime.Now.Year || DateTime.Now.Month == 1)
                {
                    daysInMonth = 0;
                    adb = 0;
                    for (int j = 1; j <= i; j++)
                    {
                        daysInMonth += DateTime.DaysInMonth(Convert.ToInt32(GetApplicableYear(i)), j);
                        adb += adbYTDList[j];
                    }
                    strYTDData.Append(adb / daysInMonth);
                }
                strYTDData.Append(",");
            }


            return strYTDData.ToString();
        }
        private string BuildHeader()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("CIF No.,Client Name,Branch Code,Branch Name,Area Name, Business Owner, Customer Segment,Category,Sub Category,Client Rank,");
            sb.AppendFormat("Industry Classification,PHP CASA Account No.,Account Type,Account Status,");

            sb.AppendFormat("{0}{1}", AdbAndYtdHeaders(), Environment.NewLine);
            return sb.ToString();
        }
        private async Task<string> BuildHeaderSummary()
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = await _repo.SelectAllProduct();

            sb.AppendFormat(",,,,,,,,,,,,,,,Product Availed");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.AppendFormat(",");
            }

            sb.AppendFormat("MTD ADB(OF ACCOUNT),,,,,,,,,,,,YTD ADB OF ACCOUNT{0}", Environment.NewLine);

            sb.AppendFormat("CIF No.,Client Name,Main Depository Branch,Area Name, Business Owner, Customer Segment,Category,Sub Category,Client Rank,");
            sb.AppendFormat("Industry Classification,No. of Active PHP CA w/in CIF,No. of Active PHP SA w/in CIF,No. of Inactive PHP CASA w/in CIF,CASH Customer TAGGING,");
            sb.AppendFormat("CIF PHP CASA TAGGING,TOTAL REQUIRED ADB,");

            foreach (DataRow item in dt.Rows)
            {
                sb.AppendFormat("{0},", item["ProductName"].ToString().Replace(",", ""));
            }



            sb.AppendFormat("{0}", AdbAndYtdHeaders());
            return sb.ToString();
        }
        private string AdbAndYtdHeaders()
        {
            StringBuilder sb = new StringBuilder();

            //MTD
            for (int i = 1; i <= 12; i++)
            {
                sb.AppendFormat("{0}-{1},", _common.GetMonthString(i), GetApplicableYear(i));
            }
            //YTD
            for (int i = 1; i <= 12; i++)
            {
                sb.AppendFormat("{0}-{1},", _common.GetMonthString(i), GetApplicableYear(i));
            }

            return sb.ToString();
        }
        private string GetApplicableYear(int month)
        {
            string year = string.Empty;

            if (DateTime.Now.Month > month)
            {
                year = DateTime.Now.Year.ToString();
            }
            else
            {
                year = DateTime.Now.AddYears(-1).Year.ToString();

            }

            return year;
        }
    }
}
