using Application.Models.DTOs.Area;
using Application.Models.DTOs.Product;
using Application.Models.Helpers;
using Application.Models.Responses;

namespace Application.Contracts.Services
{
    public interface IProductService
    {
        Task<PaginatedList<ProductDTO>> GetProductList(int Page = 1, string Search = "");
        Task<List<ProductDTO>> GetProductList();
        Task<ReturnStatus> SaveProduct(ProductDTO Product);
        Task<ReturnStatus> DeleteProduct(int id);
    }
}
