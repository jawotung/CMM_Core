using Application.Models.DTOs.Product;
using Application.Models.Helpers;
using Application.Models.Responses;
using WebAPI;

namespace Application.Contracts.Repositories
{
    public interface IProductAdbRepository
    {
        Task<PaginatedList<CmmProductAdb, ProductAdbDTO>> GetProductAdbList(int page = 1, string search = "");
        Task<List<CmmProductAdb>> GetProductAdbList();
        Task<CmmProductAdb> GetProductAdb(int ProductId,string AdbName);
        Task<CmmProductAdb> GetProductAdb(int ProductAdbId);
        Task<ReturnStatus> Update(CmmProductAdb productAdb);
        Task<ReturnStatus> Add(CmmProductAdb productAdb);
        Task<ReturnStatus> Delete(int ProductAdbId);
    }
}
