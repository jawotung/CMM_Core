using Application.Models.DTOs.Product;
using Application.Models.Helpers;
using Application.Models.Responses;
using WebAPI;

namespace Application.Contracts.Repositories
{
    public interface IProductTierRepository
    {
        Task<PaginatedList<CmmProductTier, ProductTierDTO>> GetProductTierList(int ProductAdbId, int page = 1, string search = "");
        Task<List<CmmProductTier>> GetProductTierList();
        Task<CmmProductTier> GetProductTier(int ProductAdbId, string TierName);
        Task<CmmProductTier> GetProductTierByDesc(int ProductAdbId, string TierDesc);
        Task<CmmProductTier> GetProductTier(int FeeId);
        Task<ReturnStatus> Update(CmmProductTier ProductTier);
        Task<ReturnStatus> Add(CmmProductTier ProductTier);
        Task<ReturnStatus> Delete(int FeeId);
    }
}
