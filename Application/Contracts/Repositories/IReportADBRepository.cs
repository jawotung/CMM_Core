using Application.Models.Structs;
using System.Data;

namespace Application.Contracts.Repositories
{
    public interface IReportADBRepository
    {
        Task<DataTable> SelectYTDSummary(Client details);
        Task<DataTable> SelectYTD(Client details);
        Task<DataTable> SelectAllProduct();
        Task<DataTable> GetAllAcounts(string cifNo);
        Task<DataTable> GetProductListing(Client details);
    }
}
