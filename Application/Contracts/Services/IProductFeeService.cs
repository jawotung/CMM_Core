using Application.Models.DTOs.Area;
using Application.Models.DTOs.Product;
using Application.Models.Helpers;
using Application.Models.Responses;

namespace Application.Contracts.Services
{
    public interface IProductFeeService
    {
        Task<PaginatedList<ProductFeeDTO>> GetProductFeeList(int ProductAdbId, int Page = 1, string Search = "");
        Task<List<ProductFeeDTO>> GetProductFeeList();
        Task<ReturnStatus> SaveProductFee(ProductFeeDTO productFee);
        Task<ReturnStatus> DeleteProductFee(int FeeId);
    }
}
