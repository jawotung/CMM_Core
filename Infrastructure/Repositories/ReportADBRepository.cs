using Application.Contracts.Data;
using Application.Contracts.Repositories;
using Application.Models.Structs;
using Dapper;
using System.Data;

namespace Infrastructure.Repositories
{
    public class ReportADBRepository : IReportADBRepository
    {

        private readonly ISQLDataAccess _db;
        public ReportADBRepository(ISQLDataAccess db)
        {
            _db = db;
        }
        public async Task<DataTable> SelectYTDSummary(Client details)
        {
            //mpozaeta - changed SP
            //using (DbCmd = DB.GetStoredProcCommand("SP_YTD"))
            try
            {
                var param = new DynamicParameters();
                //details.OwnedBy = new ClientData().GetOfficerID(details.OwnedBy);
                //details.CmOfficer = new ClientData().GetOfficerID(details.CmOfficer);

                param.Add("@CIFNO", details.CifNo == string.Empty ? null : details.CifNo);
                param.Add("@AREA", details.Area == string.Empty ? null : details.Area);
                param.Add("@CLIENTNAME", details.ClientName == string.Empty ? null : details.ClientName);
                param.Add("@SEGMENTID", details.ClientSegment == string.Empty ? null : details.ClientSegment);
                param.Add("@LINEOFBUSSID", details.LineOfBusiness == string.Empty ? null : details.LineOfBusiness);
                param.Add("@CATEGORYID", details.ClientCategory == string.Empty ? null : details.ClientCategory);
                param.Add("@RANK", details.Rank == string.Empty ? null : details.Rank);
                param.Add("@PRODUCTID", details.ProductID == string.Empty ? null : details.ProductID);
                param.Add("@SUBCATEGORY", details.subCategory == string.Empty ? null : details.subCategory);

                param.Add("@ACCOUNTSTATUS", details.AccountStatus == string.Empty ? null : details.AccountStatus);
                param.Add("@DEPOSITCURRENCY", details.DepositCurrency == string.Empty ? null : details.DepositCurrency);
                param.Add("@BUSOWNER", details.BusOwner == "-1" ? -1 : int.Parse(details.BusOwner));
                if (details.ProdAvailedAsOf != DateTime.MinValue)
                {
                    param.Add("@PRODDATEAVAILEDASOF", details.ProdAvailedAsOf);
                }
                else
                {
                    param.Add("@PRODDATEAVAILEDASOF", null);
                }

                if (details.ProdAvailedFrom != DateTime.MinValue)
                {
                    param.Add("@PRODDATEAVAILEDFROM", details.ProdAvailedFrom);
                }
                else
                {
                    param.Add("@PRODDATEAVAILEDFROM", null);
                }
                return await _db.LoadDataTable<dynamic>("sp_GetADBReportSummary", param);
            }
            catch 
            {
                throw;
            }
        }
        public async Task<DataTable> SelectYTD(Client details)
        {
            try
            {
                //details.OwnedBy = new ClientData().GetOfficerID(details.OwnedBy);
                //details.CmOfficer = new ClientData().GetOfficerID(details.CmOfficer);
                var param = new DynamicParameters();
                param.Add("@CLIENTNAME", details.ClientName == string.Empty ? null : details.ClientName);
                param.Add("@TYPEID", details.ClientType == string.Empty ? null : details.ClientType);
                param.Add("@SEGMENTID", details.ClientSegment == string.Empty ? null : details.ClientSegment);
                param.Add("@LINEOFBUSSID", details.LineOfBusiness == string.Empty ? null : details.LineOfBusiness);
                param.Add("@CATEGORYID", details.ClientCategory == string.Empty ? null : details.ClientCategory);
                param.Add("@ACCTOWNER", details.OwnedBy == string.Empty ? null : details.OwnedBy);
                param.Add("@CMOFFICER", details.CmOfficer == string.Empty ? null : details.CmOfficer);
                param.Add("@REQADB", details.RequiredADB);
                param.Add("@PRODUCTID", details.ProductID == string.Empty ? null : details.ProductID);
                param.Add("@POSITION", details.Position == string.Empty ? null : details.Position);
                param.Add("@CIF", details.CifNo == string.Empty ? null : details.CifNo);
                param.Add("@RANK", details.Rank == string.Empty ? null : details.Rank);
                param.Add("@BUSOWNER", details.BusOwner == "-1" ? null : details.BusOwner);
                param.Add("@SUBCATEGORY", details.subCategory == string.Empty ? null : details.subCategory);
                param.Add("@PRODDATEAVAILEDASOF", details.ProdAvailedAsOf == DateTime.MinValue ? DateTime.Now : details.ProdAvailedAsOf);

                if (details.ProdAvailedFrom != DateTime.MinValue)
                {
                    param.Add("@PRODDATEAVAILEDFROM", details.ProdAvailedFrom);
                }

                if (details.ClientStatus != -1)
                {
                    param.Add("@CLIENTSTATUS", Convert.ToBoolean(details.ClientStatus));
                }

                if (details.ProdAvailmentStatus != -1)
                {
                    //0 =
                    param.Add("@PRODAVAILSTATUS", Convert.ToBoolean(details.ProdAvailmentStatus));
                }
                return await _db.LoadDataTable<dynamic>("sp_GetAllAccountsByCIF", param);
            }
            catch 
            {
                throw;
            }
        }
        public async Task<DataTable> SelectAllProduct()
        {
            try
            {
                var param = new DynamicParameters();
                return await _db.LoadDataTable<dynamic>("SP_SelectAllProducts", param);
            }
            catch 
            {
                throw;
            }
        }
        public async Task<DataTable> GetAllAcounts(string cifNo)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CIFNO", cifNo);
                return await _db.LoadDataTable<dynamic>("sp_GetAllAccountsByCIF", param);
            }
            catch
            {
                throw;
            }
        }
        public async Task<DataTable> GetProductListing(Client details)
        {

            try
            {
                var param = new DynamicParameters();
                //details.OwnedBy = new ClientData().GetOfficerID(details.OwnedBy);
                //details.CmOfficer = new ClientData().GetOfficerID(details.CmOfficer); jawopogi

                param.Add("@CLIENTNAME", details.ClientName == string.Empty ? null : details.ClientName);
                param.Add("@TYPEID", details.ClientType == string.Empty ? null : details.ClientType);
                param.Add("@SEGMENTID", details.ClientSegment == string.Empty ? null : details.ClientSegment);
                param.Add("@LINEOFBUSSID", details.LineOfBusiness == string.Empty ? null : details.LineOfBusiness);
                param.Add("@CATEGORYID", details.ClientCategory == string.Empty ? null : details.ClientCategory);
                param.Add("@ACCTOWNER", details.OwnedBy == string.Empty ? null : details.OwnedBy);
                param.Add("@CMOFFICER", details.CmOfficer == string.Empty ? null : details.CmOfficer);
                param.Add("@REQADB", details.RequiredADB);
                param.Add("@PRODUCTID", details.ProductID == string.Empty ? null : details.ProductID);
                param.Add("@POSITION", details.Position == string.Empty ? null : details.Position);
                param.Add("@CIF", details.CifNo == string.Empty ? null : details.CifNo);
                param.Add("@RANK", details.Rank == string.Empty ? null : details.Rank);
                param.Add("@BUSOWNER", details.BusOwner == string.Empty ? null : details.BusOwner);
                param.Add("@SUBCATEGORY", details.subCategory == string.Empty ? null : details.subCategory);
                param.Add("@PRODDATEAVAILEDASOF", details.ProdAvailedAsOf == DateTime.MinValue ? DateTime.Now : details.ProdAvailedAsOf);

                if (details.ProdAvailedFrom != DateTime.MinValue)
                {
                    param.Add("@PRODDATEAVAILEDFROM", details.ProdAvailedFrom);
                }

                if (details.ClientStatus != -1)
                {
                    param.Add("@CLIENTSTATUS", Convert.ToBoolean(details.ClientStatus));
                }

                if (details.ProdAvailmentStatus != -1)
                {
                    //0 =
                    param.Add("@PRODAVAILSTATUS", Convert.ToBoolean(details.ProdAvailmentStatus));
                }

                return await _db.LoadDataTable<dynamic>("SP_GetClientProductCheckList", param);
            }
            catch
            {
                throw;
            }
        }
    }
}
