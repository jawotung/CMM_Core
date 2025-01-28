using Application.Models.DTOs.Product;
using Application.Models.Helpers;
using Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Services
{
    public interface IProductUdfService
    {
        Task<PaginatedList<ProductUdfDTO>> GetProductUdfList(int ProductId, int Page = 1, string Search = "");
        Task<List<ProductUdfDTO>> GetProductUdfList();
        Task<ReturnStatus> SaveProductUdf(ProductUdfDTO productUdf);
        Task<ReturnStatus> DeleteProductUdf(Guid id);
    }
}
