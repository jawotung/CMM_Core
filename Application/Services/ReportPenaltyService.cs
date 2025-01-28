using Application.Contracts.Repositories;
using Application.Contracts.Services;
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
    public class ReportPenaltyService : IReportPenaltyService
    {

        private readonly IReportPenaltyRepository _repo;
        private readonly CommonClass _common;
        public ReportPenaltyService(IReportPenaltyRepository repo, CommonClass common) 
        {
            _repo = repo;
            _common = common;
        }
        public async Task<ReturnStatusData<ReturnDownload>> GenerateReport(Client clientInfo)
        {
            ReturnStatusData<ReturnDownload> result = new(new());
            try
            {
                string content = await ProcessDetailedADBPenaltyReport(clientInfo);
                if (content.Trim().Length > 0)
                {
                    string filename = string.Format("ShortfallReport{0}.csv", Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10));
                    _common.GenerateFileCSV(content);
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        public async Task<string> ProcessDetailedADBPenaltyReport(Client clientDetails)
        {
            List<ClientReportDetails> penaltyDetails = await GetPenaltyDetails(clientDetails);
            string content = BuildPenaltyReport(penaltyDetails, clientDetails.MeetingRequiredADB);

            return content;
        }
        public string BuildPenaltyReport(List<ClientReportDetails> reportDetails, int meetingReqADB)
        {
            StringBuilder sb = new StringBuilder();
            int year, month;
            month = (DateTime.Now.Month - 1 == 0 ? 12 : DateTime.Now.Month - 1);
            year = (month == 12 ? DateTime.Now.Year - 1 : DateTime.Now.Year);
            sb.AppendFormat("BANK OF COMMERCE{0}", Environment.NewLine);
            sb.AppendFormat("CASH MANAGEMENT MONITORING SYSTEM{0}", Environment.NewLine);
            sb.AppendFormat("CASH CUSTOMER - ADB SHORTFALL REPORT - {0}{1}", GetPenaltyReportTitle(meetingReqADB), Environment.NewLine);
            sb.AppendFormat("REPORT MONTH DATE: AS OF {0}-{1}{2}", _common.GetMonthString(month), year, Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.AppendFormat("{0}{1}", BuildHeader(), Environment.NewLine);
            sb = BuildData(reportDetails, sb);


            return sb.ToString();
        }
        private string GetPenaltyReportTitle(int meetingReqADB)
        {
            string title = string.Empty;

            switch (meetingReqADB)
            {
                case -1:
                    title = "ALL";
                    break;
                case 0:
                    title = "BELOW REQUIRED ADB";
                    break;
                case 1:
                    title = "MEETING REQUIRED ADB ";
                    break;
            }

            return title;
        }
        private StringBuilder BuildClientProductData(List<ClientReportDetails> reportDetails, StringBuilder sb, List<ProductCheckStatus> productListing)
        {
            foreach (var item in reportDetails)
            {
                sb.AppendFormat("{0},", item.ClientCIF);
                sb.AppendFormat("{0},", item.ClientName);
                sb.AppendFormat("{0},", item.CustomerType);
                sb.AppendFormat("{0},", item.SegmentName);
                sb.AppendFormat("{0},", item.SubCategory);
                sb.AppendFormat("{0},", item.ClientRank);
                sb.AppendFormat("{0},", item.IndustryClass);
                sb.AppendFormat("{0},", item.MainBranch);
                sb.AppendFormat("{0},", item.Area);
                sb.AppendFormat("{0},", "");
                sb.AppendFormat("{0},", item.CMOfficer);
                sb.AppendFormat("{0},", item.RequiredADB);
                sb.AppendFormat("{0},", item.IsCustomerActive);


                var prod = from s in productListing
                           where s.CIFNo == item.ClientCIF
                           select s;

                sb.AppendFormat("{0},", prod.First().Payroll);
                sb.AppendFormat("{0},", prod.First().Payment);
                sb.AppendFormat("{0},", prod.First().CheckCutting);
                sb.AppendFormat("{0},", prod.First().CheckwriterPlus);
                sb.AppendFormat("{0},", prod.First().PDCWareHousing);
                sb.AppendFormat("{0},", prod.First().BillsPaymentFacility);
                sb.AppendFormat("{0},", prod.First().BancnetBillsPayment);
                sb.AppendFormat("{0},", prod.First().BancNetPOS);
                sb.AppendFormat("{0},", prod.First().BancnetEMerchant);
                sb.AppendFormat("{0},", prod.First().DepositPickup);
                sb.AppendFormat("{0},", prod.First().CIB);
                sb.AppendFormat("{0},", prod.First().eFPS);
                sb.AppendFormat("{0},", prod.First().SweepFacility);
                sb.AppendFormat("{0},", prod.First().ImportPAS5);
                sb.AppendFormat("{0},", prod.First().ExportPAS5);
                sb.AppendFormat("{0},", prod.First().PayrollPlus);



                sb.AppendFormat("{0}", Environment.NewLine);
                //Total ADB
                //Products
            }

            return sb;
        }
        private StringBuilder BuildData(List<ClientReportDetails> reportDetails, StringBuilder sb)
        {
            foreach (var item in reportDetails)
            {
                if ((item.Line == "DETAIL LINE"))
                {
                    string acctno = item.MonitoringAcct == null ? new String(' ', 12) : item.MonitoringAcct.PadLeft(12, '0');
                    if ((acctno.Substring(3, 2) == "00" || acctno.Substring(3, 2) == "20"))
                    {
                        sb.AppendFormat("{0},", item.ClientCIF);
                        sb.AppendFormat("{0},", item.ProductCIF);
                        sb.AppendFormat("{0},", item.ClientName);
                        sb.AppendFormat("{0},", item.MainBranch);
                        sb.AppendFormat("{0},", item.Area);
                        sb.AppendFormat("{0},", item.Line);
                        sb.AppendFormat("{0},", item.ProductName);
                        sb.AppendFormat("{0},", item.MonitoringAcct);
                        sb.AppendFormat("{0},", item.RequiredADB);
                        sb.AppendFormat("{0},", item.MtdAdb);
                        sb.AppendFormat("{0},", item.ShortFall);
                        sb.AppendFormat("{0},", item.DebitAcct);
                        sb.AppendFormat("{0},", item.CollectionMethod);
                        sb.AppendFormat("{0},", string.Empty);
                        sb.AppendFormat("{0}", Environment.NewLine);
                    }
                }
                else if (item.Line == "TOTAL LINE")
                {
                    sb.AppendFormat("{0},", item.ClientCIF);
                    sb.AppendFormat("{0},", item.ProductCIF);
                    sb.AppendFormat("{0},", item.ClientName);
                    sb.AppendFormat("{0},", item.MainBranch);
                    sb.AppendFormat("{0},", item.Area);
                    sb.AppendFormat("{0},", item.Line);
                    sb.AppendFormat("{0},", item.ProductName);
                    sb.AppendFormat("{0},", item.MonitoringAcct);
                    sb.AppendFormat("{0},", item.RequiredADB);
                    sb.AppendFormat("{0},", item.MtdAdb);
                    sb.AppendFormat("{0},", item.ShortFall);
                    sb.AppendFormat("{0},", item.DebitAcct);
                    sb.AppendFormat("{0},", item.CollectionMethod);
                    sb.AppendFormat("{0},", string.Empty);
                    sb.AppendFormat("{0}", Environment.NewLine);
                }
            }

            return sb;
        }
        private string BuildHeader()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("CIF No.,CIF of Entered Account No.,Client Name,Main Depository Branch,Area Name,Line,Cash Product,Monitoring Account No.,");
            sb.AppendFormat("Required ADB,MTD ADB, Shortfall MTD ADB, Nominated CASA for Penalty,Manner of Collecting Penalty Charge,Remarks");


            return sb.ToString();
        }
        public async Task<List<ClientReportDetails>> GetPenaltyDetails(Client clientDetails)
        {
            DataTable dtPenaltyDetails = await _repo.GetDetailedPenaltyItems(clientDetails);
            List<ClientReportDetails> penaltyDetails = TransformToClientReportDetails(dtPenaltyDetails, clientDetails.MeetingRequiredADB);
            return penaltyDetails;
        }
        private List<ClientReportDetails> TransformToClientReportDetails(DataTable dtPenaltyDetails, int isShortFall)
        {
            List<ClientReportDetails> transformedDetails = new List<ClientReportDetails>();

            ClientReportDetails detail;

            foreach (DataRow row in dtPenaltyDetails.Rows)
            {
                //    decimal tempShortFall = decimal.Parse(row["MTD ADB"].ToString().Replace(",", ""))
                //        - (string.IsNullOrEmpty(row["Required ADB"].ToString()) ? 0 : decimal.Parse(row["Required ADB"].ToString()));
                detail = new ClientReportDetails();
                //    if (tempShortFall >= 0 && isShortFall == 1)
                //        AddTranformedDetails(detail, row, transformedDetails);
                //    else if (tempShortFall < 0 && isShortFall == 0)
                //        AddTranformedDetails(detail, row, transformedDetails);
                //    else if( isShortFall == -1)
                //        AddTranformedDetails(detail, row, transformedDetails);
                AddTranformedDetails(detail, row, transformedDetails);
            }

            transformedDetails = ComputeTotalsPerCIF(transformedDetails);
            transformedDetails = ApplyShortfallFilter(transformedDetails, isShortFall);
            return transformedDetails;
        }
        private List<ClientReportDetails> ApplyShortfallFilter(List<ClientReportDetails> transformedDetails, int isShortFall)
        {
            List<ClientReportDetails> shortFallFilter = new List<ClientReportDetails>();

            if (isShortFall == 1)
            {
                var filter = from p in transformedDetails
                             where p.Line == "TOTAL LINE" && p.ShortFall >= 0
                             select p.ClientCIF;
                List<string> cifList = filter.ToList<string>();

                var filteredLIst = transformedDetails.Where(s => cifList.Contains(s.ClientCIF));
                shortFallFilter = filteredLIst.ToList<ClientReportDetails>();
            }
            else if (isShortFall == 0)
            {
                var filter = from p in transformedDetails
                             where p.Line == "TOTAL LINE" && p.ShortFall < 0
                             select p.ClientCIF;
                List<string> cifList = filter.ToList<string>();

                var filteredLIst = transformedDetails.Where(s => cifList.Contains(s.ClientCIF));
                shortFallFilter = filteredLIst.ToList<ClientReportDetails>();
            }
            else if (isShortFall == -1)
            {
                shortFallFilter = transformedDetails;
            }

            return shortFallFilter;
        }

        private void AddTranformedDetails(ClientReportDetails Detail, DataRow Row, List<ClientReportDetails> TransformedDetails)
        {

            Detail.Area = Row["AreaName"].ToString().Replace(",", "");
            Detail.MainBranch = Row["Depository Branch"].ToString().Replace(",", "");
            Detail.ClientCIF = Row["ClientCIF"].ToString().Replace(",", "");
            Detail.ProductCIF = Row["ProductCIF"].ToString().Replace(",", "");
            Detail.ClientName = Row["clientname"].ToString().Replace(",", "");
            Detail.CollectionMethod = Row["CollectionMethod"].ToString().Replace(",", "");
            Detail.DebitAcct = Row["DebitAcct"].ToString().Replace(",", "");
            Detail.IsProductActive = Row["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(Row["IsActive"]);
            Detail.Line = Row["Line"].ToString().Replace(",", "");
            Detail.MonitoringAcct = Row["MonitorAcct"].ToString().Replace(",", "");
            Detail.MtdAdb = decimal.Parse(Row["MTD ADB"].ToString().Replace(",", ""));
            Detail.ProductName = Row["ProductName"].ToString().Replace(",", "");
            Detail.RequiredADB = string.IsNullOrEmpty(Row["Required ADB"].ToString()) ? 0 : decimal.Parse(Row["Required ADB"].ToString());
            Detail.ProductBranch = Row["Product Branch"].ToString().Replace(",", "");
            Detail.ShortFall = (Detail.MtdAdb - Detail.RequiredADB) >= 0 ? 0 : (Detail.MtdAdb - Detail.RequiredADB);

            TransformedDetails.Add(Detail);
        }

        public List<ClientReportDetails> ComputeTotalsPerCIF(List<ClientReportDetails> rawDetails)
        {
            List<ClientReportDetails> detailsWithTotal = new List<ClientReportDetails>();
            ClientReportDetails totalDetail;
            decimal totalReqAdb;
            decimal totalMtdAdb;

            var orderByReqADB =
                from p in rawDetails
                orderby p.ClientName, p.RequiredADB descending
                select p;

            //order by cifno
            var orderByCifNo =
                from p in orderByReqADB
                group p by p.ClientCIF into g
                select new { CIFNo = g.Key, Details = g };

            foreach (var item in orderByCifNo)
            {
                totalMtdAdb = 0;
                totalReqAdb = 0;

                totalDetail = new ClientReportDetails();
                totalDetail.MainBranch = item.Details.First().MainBranch;
                totalDetail.Line = "TOTAL LINE";
                totalDetail.ClientName = item.Details.First().ClientName;
                totalDetail.ClientCIF = item.CIFNo;
                totalDetail.ProductCIF = item.Details.First().ProductCIF;
                totalDetail.Area = item.Details.First().Area;

                var mtd =
                    from m in item.Details
                    group m by m.MonitoringAcct into n
                    select new { MtdAcctNo = n.Key, Balance = n.First().MtdAdb };

                foreach (var row in mtd)
                {
                    totalMtdAdb += row.Balance;
                }

                foreach (var accountDetail in item.Details)
                {
                    totalReqAdb += accountDetail.RequiredADB;

                    var check =
                        from p in detailsWithTotal
                        where p.ClientCIF == accountDetail.ClientCIF && accountDetail.MonitoringAcct == p.MonitoringAcct &&
                        p.ProductName == accountDetail.ProductName
                        select p;

                    if (check.Count() == 0)
                    {
                        if (detailsWithTotal.Count(s => s.MonitoringAcct == accountDetail.MonitoringAcct) == 0)
                        {
                            detailsWithTotal.Add(accountDetail);
                        }
                        else if (accountDetail.ProductName != string.Empty)
                        {
                            detailsWithTotal.Add(accountDetail);
                        }
                    }
                }

                totalDetail.MtdAdb = totalMtdAdb;
                totalDetail.RequiredADB = totalReqAdb;
                totalDetail.ShortFall = ((totalMtdAdb - totalReqAdb) >= 0) ? 0 : (totalMtdAdb - totalReqAdb);

                detailsWithTotal.Add(totalDetail);
            }


            return detailsWithTotal;
        }
    }
}
