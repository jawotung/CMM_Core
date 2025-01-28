using Application.Models.DTOs.Product;
using Application.Models.Helpers;
using Application.Models.Responses;
using WebAPI;

namespace Application.Contracts.Repositories
{
    public interface IProductFeeRepository
    {
        Task<PaginatedList<CmmProductFee, ProductFeeDTO>> GetProductFeeList(int ProductAdbId, int page = 1, string search = "");
        Task<List<CmmProductFee>> GetProductFeeList();
        Task<CmmProductFee> GetProductFee(int ProductAdbId,int TierId, string FeeName);
        Task<CmmProductFee> GetProductFee(int FeeId);
        Task<ReturnStatus> Update(CmmProductFee productFee);
        Task<ReturnStatus> Add(CmmProductFee productFee);
        Task<ReturnStatus> Delete(int FeeId);
    }
}
