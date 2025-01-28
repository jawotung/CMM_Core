using Application.Contracts.Repositories;
using Application.Contracts.Services;
using Application.Models.DTOs.Product;
using Application.Models.Helpers;
using Application.Models.Responses;
using AutoMapper;
using WebAPI;

namespace Application.Services
{
    public class ProductTierService : IProductTierService
    {
        private readonly IProductTierRepository _repo;
        private readonly IMapper _mapper;
        public ProductTierService(IProductTierRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<PaginatedList<ProductTierDTO>> GetProductTierList(int ProductAdbId,int Page = 1, string Search = "")
        {
            PaginatedList<CmmProductTier, ProductTierDTO> data = await _repo.GetProductTierList(ProductAdbId, Page, Search);
            return new PaginatedList<ProductTierDTO>(data.Data, data.PageIndex, data.TotalPages, data.CountData);
        }
        public async Task<List<ProductTierDTO>> GetProductTierList()
        {
            List<CmmProductTier> data = await _repo.GetProductTierList();
            return _mapper.Map<List<ProductTierDTO>>(data);
        }
        public async Task<ReturnStatus> SaveProductTier(ProductTierDTO productTier)
        {
            ReturnStatus result = new();
            try
            {
                CmmProductTier data = _mapper.Map<CmmProductTier>(productTier);
                if (await ProductTierNameExists(data.ProductAdbId, data.TierId, data.TierName))
                {
                    result.Message = "Tier Name already existing";
                    return result;
                }
                if (await ProductTierDescExists(data.ProductAdbId, data.TierId, data.TierDesc))
                {
                    result.Message = "Tier Desc already existing";
                    return result;
                }
                if (productTier.TierId == 0)
                    result = await _repo.Add(data);
                else
                {
                    var existing = await _repo.GetProductTier(productTier.TierId);
                    if (existing.TierId == 0)
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
        public async Task<ReturnStatus> DeleteProductTier(int TierId)
        {
            ReturnStatus result = new();
            try
            {
                var ADBFee = await _repo.GetProductTier(TierId);
                if (ADBFee.TierId == 0)
                {
                    result.Message = "No data found";
                    return result;
                }
                result = await _repo.Delete(TierId);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        private async Task<bool> ProductTierNameExists(int ProductAdbId, int TierId, string TierName)
        {
            var data = await _repo.GetProductTier(ProductAdbId, TierName);
            return data != null && (TierId != 0 ? (data.TierId != TierId && data.TierName == TierName) : data.TierName == TierName);
        }
        private async Task<bool> ProductTierDescExists(int ProductAdbId, int TierId, string TierDesc)
        {
            var data = await _repo.GetProductTierByDesc(ProductAdbId, TierDesc);
            return data != null && (TierId != 0 ? (data.TierId != TierId && data.TierDesc == TierDesc) : data.TierDesc == TierDesc);
        }
    }
}
