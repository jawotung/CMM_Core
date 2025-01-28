using Application.Contracts.Repositories;
using Application.Models.DTOs.Product;
using Application.Models.Helpers;
using Application.Models.Responses;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI;

namespace Application.Services
{
    public class ProductFeeService
    {
        private readonly IProductFeeRepository _repo;
        private readonly IMapper _mapper;
        public ProductFeeService(IProductFeeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<PaginatedList<ProductFeeDTO>> GetProductFeeList(int Page = 1, string Search = "")
        {
            PaginatedList<CmmProductFee, ProductFeeDTO> data = await _repo.GetProductFeeList(Page, Search);
            return new PaginatedList<ProductFeeDTO>(data.Data, data.PageIndex, data.TotalPages, data.CountData);
        }
        public async Task<List<ProductFeeDTO>> GetProductFeeList()
        {
            List<CmmProductFee> data = await _repo.GetProductFeeList();
            return _mapper.Map<List<ProductFeeDTO>>(data);
        }
        public async Task<ReturnStatus> SaveProductFee(ProductFeeDTO productFee)
        {
            ReturnStatus result = new();
            try
            {
                CmmProductFee data = _mapper.Map<CmmProductFee>(productFee);
                if (await ProductFeeNameExists(data.ProductAdbId, data.FeeId, data.FeeName))
                {
                    result.Message = "ADB Name already existing";
                    return result;
                }
                if (productFee.FeeId == 0)
                    result = await _repo.Add(data);
                else
                {
                    var existing = await _repo.GetProductFee(productFee.FeeId);
                    if (existing.FeeId == 0)
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
        public async Task<ReturnStatus> DeleteProductFee(int FeeId)
        {
            ReturnStatus result = new();
            try
            {
                var ADBFee = await _repo.GetProductFee(FeeId);
                if (ADBFee.FeeId == 0)
                {
                    result.Message = "No data found";
                    return result;
                }
                result = await _repo.Delete(FeeId);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        private async Task<bool> ProductFeeNameExists(int ProductAdbId, int FeeId, string FeeName)
        {
            var data = await _repo.GetProductFee(ProductAdbId, FeeName);
            return data != null && (FeeId != 0 ? (data.FeeId != FeeId && data.FeeName == FeeName) : data.FeeName == FeeName);
        }
    }
}
