using Application.Contracts.Repositories;
using Application.Models.DTOs.Product;
using Application.Models.Helpers;
using Application.Models.Responses;
using AutoMapper;
using WebAPI;

namespace Application.Services
{
    public class ProductADBService
    {
        private readonly IProductAdbRepository _repo;
        private readonly IMapper _mapper;
        public ProductADBService(IProductAdbRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<PaginatedList<ProductAdbDTO>> GetProductAdbList(int ProductId, int Page = 1, string Search = "")
        {
            PaginatedList<CmmProductAdb, ProductAdbDTO> data = await _repo.GetProductAdbList(ProductId, Page, Search);
            return new PaginatedList<ProductAdbDTO>(data.Data, data.PageIndex, data.TotalPages, data.CountData);
        }
        public async Task<List<ProductAdbDTO>> GetProductAdbList()
        {
            List<CmmProductAdb> data = await _repo.GetProductAdbList();
            return _mapper.Map<List<ProductAdbDTO>>(data);
        }
        public async Task<ReturnStatus> SaveProductAdb(ProductAdbDTO productAdb)
        {
            ReturnStatus result = new();
            try
            {
                CmmProductAdb data = _mapper.Map<CmmProductAdb>(productAdb);
                if (await ProductAdbNameExists(data.ProductId, data.ProductAdbId, data.AdbName))
                {
                    result.Message = "ADB Name already existing";
                    return result;
                }
                if (productAdb.ProductAdbId == 0)
                    result = await _repo.Add(data);
                else
                {
                    var existing = await _repo.GetProductAdb(productAdb.ProductAdbId);
                    if (existing.ProductAdbId == 0)
                    {
                        result.Message = "No data found";
                        return result;
                    }
                    result = await _repo.Update(data);
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }
        public async Task<ReturnStatus> DeleteProductAdb(int ProductAdbId)
        {
            ReturnStatus result = new();
            try
            {
                var ADBFee = await _repo.GetProductAdb(ProductAdbId);
                if (ADBFee.ProductAdbId == 0)
                {
                    result.Message = "No data found";
                    return result;
                }
                result = await _repo.Delete(ProductAdbId);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        private async Task<bool> ProductAdbNameExists(int ProductId, int ProductAdbId, string AdbName)
        {
            var data = await _repo.GetProductAdb(ProductId,AdbName);
            return data != null && (ProductAdbId != 0 ? (data.ProductAdbId != ProductAdbId && data.AdbName == AdbName) : data.AdbName == AdbName);
        }
    }
}
