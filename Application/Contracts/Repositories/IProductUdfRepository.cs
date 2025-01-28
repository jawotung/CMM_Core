using Application.Models.DTOs.Product;
using Application.Models.Helpers;
using Application.Models.Responses;
using WebAPI;

namespace Application.Contracts.Repositories
{
    public interface IProductUdfRepository
    {
        Task<PaginatedList<CmmUdfTemplate, ProductUdfDTO>> GetProductUdfList(int ProductId, int page = 1, string search = "");
        Task<List<CmmUdfTemplate>> GetProductUdfList();
        Task<CmmUdfTemplate> GetProductUdf(int ProductId, string UdfLabel);
        Task<CmmUdfTemplate> GetProductUdf(Guid UdfItemId);
        Task<ReturnStatus> Update(CmmUdfTemplate productUdf);
        Task<ReturnStatus> Add(CmmUdfTemplate productUdf);
        Task<ReturnStatus> Delete(Guid UdfItemId);
    }
}
