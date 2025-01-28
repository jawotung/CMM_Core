using Application.Models.DTOs.Product;
using Application.Models.Helpers;
using Application.Models.Responses;
using WebAPI;

namespace Application.Contracts.Repositories
{
    public interface IProductRepository
    {
        Task<PaginatedList<CmmProduct, ProductDTO>> GetProductList(int page = 1, string search = "");
        Task<List<CmmProduct>> GetProductList();
        Task<CmmProduct> GetProduct(string ProductCode);
        Task<CmmProduct> GetProduct(int ProductID);
        Task<CmmProduct> GetProductByProductName(string ProductName);
        Task<ReturnStatus> Update(CmmProduct Product);
        Task<ReturnStatus> Add(CmmProduct Product);
        Task<ReturnStatus> Delete(int ProductID);
    }
}
