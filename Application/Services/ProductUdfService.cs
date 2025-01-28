using Application.Contracts.Repositories;
using Application.Models.DTOs.Product;
using Application.Models.Helpers;
using Application.Models.Responses;
using AutoMapper;
using WebAPI;

namespace Application.Services
{
    public class ProductUdfService
    {
        private readonly IProductUdfRepository _repo;
        private readonly IMapper _mapper;
        public ProductUdfService(IProductUdfRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<PaginatedList<ProductUdfDTO>> GetProductUdfList(int ProductId, int Page = 1, string Search = "")
        {
            PaginatedList<CmmUdfTemplate, ProductUdfDTO> data = await _repo.GetProductUdfList(ProductId, Page, Search);
            return new PaginatedList<ProductUdfDTO>(data.Data, data.PageIndex, data.TotalPages, data.CountData);
        }
        public async Task<List<ProductUdfDTO>> GetProductUdfList()
        {
            List<CmmUdfTemplate> data = await _repo.GetProductUdfList();
            return _mapper.Map<List<ProductUdfDTO>>(data);
        }
        public async Task<ReturnStatus> SaveProductUdf(ProductUdfDTO productUdf)
        {
            ReturnStatus result = new();
            try
            {
                CmmUdfTemplate data = _mapper.Map<CmmUdfTemplate>(productUdf);
                if (await ProductUdfNameExists(data.ProductId, data.UdfItemId, data.UdfLabel))
                {
                    result.Message = "Field Label already existing";
                    return result;
                }
                if (productUdf.UdfItemId == Guid.Empty)
                    result = await _repo.Add(data);
                else
                {
                    var existing = await _repo.GetProductUdf(productUdf.UdfItemId);
                    if (existing.UdfItemId == Guid.Empty)
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
        public async Task<ReturnStatus> DeleteProductUdf(Guid UdfItemId)
        {
            ReturnStatus result = new();
            try
            {
                var ADBFee = await _repo.GetProductUdf(UdfItemId);
                if (ADBFee.UdfItemId == Guid.Empty)
                {
                    result.Message = "No data found";
                    return result;
                }
                result = await _repo.Delete(UdfItemId);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        private async Task<bool> ProductUdfNameExists(int ProductId, Guid UdfItemId, string UdfLabel)
        {
            var data = await _repo.GetProductUdf(ProductId, UdfLabel);
            return data != null && (UdfItemId != Guid.Empty ? (data.UdfItemId != UdfItemId && data.UdfLabel == UdfLabel) : data.UdfLabel == UdfLabel);
        }
    }
}
