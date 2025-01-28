using Application.Contracts.Repositories;
using Application.Contracts.Services;
using Application.Models.DTOs.Product;
using Application.Models.Helpers;
using Application.Models.Responses;
using AutoMapper;
using WebAPI;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<PaginatedList<ProductDTO>> GetProductList(int Page = 1,string Search = "")
        {
            PaginatedList<CmmProduct, ProductDTO> data = await _repo.GetProductList(Page, Search);
            return new PaginatedList<ProductDTO>(data.Data, data.PageIndex, data.TotalPages, data.CountData);
        }
        public async Task<List<ProductDTO>> GetProductList()
        {
            List<CmmProduct> data = await _repo.GetProductList();
            return _mapper.Map<List<ProductDTO>>(data);
        }
        public async Task<ReturnStatus> SaveProduct(ProductDTO Product)
        {
            ReturnStatus result = new();
            try
            {
                CmmProduct data = _mapper.Map<CmmProduct>(Product);
                if (await ProductCodeExists(data.ProductId, data.ProductCode))
                {
                    result.Message = "Product Code already existing";
                    return result;
                }
                if (await ProductNameExists(data.ProductId, data.ProductName))
                {
                    result.Message = "Product Name already existing";
                    return result;
                }
                if (Product.ProductId == 0)
                    result = await _repo.Add(data);
                else
                {
                    var existing = await _repo.GetProduct(Product.ProductId);
                    if (existing.ProductId == 0)
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
        public async Task<ReturnStatus> DeleteProduct(int id)
        {
            ReturnStatus result = new();
            try
            {
                var Product = await _repo.GetProduct(id);
                if (Product.ProductId == 0)
                {
                    result.Message = "No data found";
                    return result;
                }
                result = await _repo.Delete(id);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        private async Task<bool> ProductCodeExists(int ProductId, string ProductCode)
        {
            var data = await _repo.GetProduct(ProductCode);
            return data != null && (ProductId != 0 ? (data.ProductId != ProductId && data.ProductCode == ProductCode) : data.ProductCode == ProductCode);
        }
        private async Task<bool> ProductNameExists(int ProductId, string ProductName)
        {
            var data = await _repo.GetProduct(ProductName);
            return data != null && (ProductId != 0 ? (data.ProductId != ProductId && data.ProductName == ProductName) : data.ProductName == ProductName);
        }
    } 
}
