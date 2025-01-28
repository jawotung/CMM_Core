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
    public interface IProductAdbService
    {
        Task<PaginatedList<ProductAdbDTO>> GetProductAdbList(int Page = 1, string Search = "");
        Task<List<ProductAdbDTO>> GetProductAdbList();
        Task<ReturnStatus> SaveProductAdb(ProductAdbDTO productAdb);
        Task<ReturnStatus> DeleteProductAdb(int id);
    }
}
