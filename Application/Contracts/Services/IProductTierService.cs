using Application.Models.DTOs.Area;
using Application.Models.DTOs.Product;
using Application.Models.Helpers;
using Application.Models.Responses;

namespace Application.Contracts.Services
{
    public interface IProductTierService
    {
        Task<PaginatedList<ProductTierDTO>> GetProductTierList(int ProductAdbId, int Page = 1, string Search = "");
        Task<List<ProductTierDTO>> GetProductTierList();
        Task<ReturnStatus> SaveProductTier(ProductTierDTO productTier);
        Task<ReturnStatus> DeleteProductTier(int TierId);
    }
}
