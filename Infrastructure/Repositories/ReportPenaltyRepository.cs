using Application.Contracts.Data;
using Application.Models.Structs;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ReportPenaltyRepository
    {
        private readonly ISQLDataAccess _db;
        public ReportPenaltyRepository(ISQLDataAccess db)
        {
            _db = db;
        }
        public async Task<DataTable> GetDetailedPenaltyItems(Client details)
        {
            try
            {
                var param = new DynamicParameters();
                //details.OwnedBy = new ClientData().GetOfficerID(details.OwnedBy);
                //details.CmOfficer = new ClientData().GetOfficerID(details.CmOfficer);

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
                else
                {
                    param.Add("@PRODDATEAVAILEDFROM", null);
                }

                if (details.ClientStatus != -1)
                {
                    param.Add("@CLIENTSTATUS", Convert.ToBoolean(details.ClientStatus));
                }
                else
                {
                    param.Add("@CLIENTSTATUS", null);
                }

                if (details.ProdAvailmentStatus != -1)
                {
                    //0 =
                    param.Add("@PRODAVAILSTATUS", Convert.ToBoolean(details.ProdAvailmentStatus));
                }
                else
                {
                    param.Add("@PRODAVAILSTATUS", null);
                }
                return await _db.LoadDataTable<dynamic>("SP_GetDetailedADBPenalty", param);
            }
            catch
            {
                throw;
            }
        }
    }
}
