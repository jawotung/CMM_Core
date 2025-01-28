using Application.Contracts.Data;
using Application.Contracts.Repositories;
using Application.Models.Structs;
using Dapper;
using System.Data;

namespace Infrastructure.Repositories
{
    public class ReportPaymentRepository : IReportPaymentRepository
    {

        private readonly ISQLDataAccess _db;
        public ReportPaymentRepository(ISQLDataAccess db)
        {
            _db = db;
        }
        public async Task<DataTable> GetPaymentDetailedReport(Payment details)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CLIENTNAME", details.ClientName == string.Empty ? null : details.ClientName);
                param.Add("@TYPEID", details.ClientType == string.Empty ? null : details.ClientType);
                param.Add("@SEGMENTID", details.ClientSegment == string.Empty ? null : details.ClientSegment);
                param.Add("@SUBCATEGORYID", details.ClientSubCategory == string.Empty ? null : details.ClientSubCategory);
                param.Add("@CLIENTRANK", details.ClientRank == string.Empty ? null : details.ClientRank);
                param.Add("@INDUSCLASS", details.IndustryClassification == string.Empty ? null : details.IndustryClassification);
                param.Add("@BRANCH", details.Branch == string.Empty ? null : details.Branch);
                param.Add("@AREA", details.Area == string.Empty ? null : details.Area);
                param.Add("@BUSOWNER", details.BusinessOwner == string.Empty ? null : details.BusinessOwner);

                if (details.MonthAvailmentFrom == DateTime.MinValue)
                {
                    param.Add("@MONTHAVAILFROM", null);
                }
                else
                {
                    param.Add("@MONTHAVAILFROM", details.MonthAvailmentFrom);
                }

                if (details.MonthAvailmentTo == DateTime.MinValue)
                {
                    param.Add("@MONTHAVAILTO", null);
                }
                else
                {
                    param.Add("@MONTHAVAILTO", details.MonthAvailmentTo);
                }
                param.Add("@PRICINGOPTIONS", details.PricingOptions == string.Empty ? null : details.PricingOptions);
                param.Add("@PRODUCTSTATUS", details.ProductStatus);
                param.Add("@PRODUCTID", details.ProductID == string.Empty ? null : details.ProductID);

                return await _db.LoadDataTable<dynamic>("sp_GetPayrollPaymentDetailed", param);
            }
            catch
            {
                throw;
            }
        }
        public async Task<DataTable> GetPaymentSummaryReport(Payment details)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CLIENTNAME", details.ClientName == string.Empty ? null : details.ClientName);
                param.Add("@TYPEID", details.ClientType == string.Empty ? null : details.ClientType);
                param.Add("@SEGMENTID", details.ClientSegment == string.Empty ? null : details.ClientSegment);
                param.Add("@SUBCATEGORY", details.ClientSubCategory == string.Empty ? null : details.ClientSubCategory);
                param.Add("@RANK", details.ClientRank == string.Empty ? null : details.ClientRank);
                param.Add("@LINEOFBUSSID", details.IndustryClassification == string.Empty ? null : details.IndustryClassification);
                param.Add("@BRANCH", details.Branch == string.Empty ? null : details.Branch);
                param.Add("@AREA", details.Area == string.Empty ? null : details.Area);
                param.Add("@BUSOWNER", details.BusinessOwner == string.Empty ? null : details.BusinessOwner);
                param.Add("@PRODUCTID", details.ProductID == string.Empty ? null : details.ProductID);
                return await _db.LoadDataTable<dynamic>("sp_GetPayrollPaymentSummary", param);
            }
            catch 
            {
                throw;
            }
        }

    }
}
