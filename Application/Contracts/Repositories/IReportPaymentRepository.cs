using Application.Models.Structs;
using System.Data;

namespace Application.Contracts.Repositories
{
    public interface IReportPaymentRepository
    {
        Task<DataTable> GetPaymentDetailedReport(Payment details);
        Task<DataTable> GetPaymentSummaryReport(Payment details);
    }
}
