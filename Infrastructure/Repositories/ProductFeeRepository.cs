using Application.Contracts.Repositories;
using Application.Models.DTOs.Area;
using Application.Models.DTOs.Product;
using Application.Models.Helpers;
using Application.Models.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI;

namespace Infrastructure.Repositories
{
    public class ProductFeeRepository : IProductFeeRepository
    {
        private readonly CMMDBContext _context;
        private readonly IMapper _mapper;
        public ProductFeeRepository(CMMDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<CmmProductFee, ProductFeeDTO>> GetProductFeeList(int page = 1, string search = "")
        {
            IQueryable<CmmProductFee> list = _context.CmmProductFees
                                        .Where(x => string.IsNullOrEmpty(search) ||
                                                    x.FeeName.Contains(search) ||
                                                    x.FeeAmount.Contains(search))
                                        .OrderBy(x => x.FeeName);
            return await PaginatedList<CmmProductFee, ProductFeeDTO>.CreateAsync(list, _mapper, page);
        }
        public async Task<List<CmmProductFee>> GetProductFeeList()
        {
            return await _context.CmmProductFees.ToListAsync();
        }
        public async Task<CmmProductFee> GetProductFee(int ProductAdbId, string FeeName)
        {
            return await _context.CmmProductFees.Where(x => x.FeeName == FeeName && x.ProductAdbId == ProductAdbId).FirstOrDefaultAsync() ?? new();
        }
        public async Task<CmmProductFee> GetProductFee(int FeeId)
        {
            return await _context.CmmProductFees.FindAsync(FeeId) ?? new();
        }
        public async Task<ReturnStatus> Update(CmmProductFee ProductFee)
        {
            ReturnStatus result = new();
            try
            {
                var existing = await _context.CmmProductFees.FindAsync(ProductFee.FeeId);
                if (existing == null)
                {
                    result.Message = "No data found";
                    return result;
                }

                _mapper.Map(ProductFee, existing);
                _context.Entry(existing).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                result.Success = true;
                result.Message = "Successfull! Data was successfully updated";
            }
            catch (DbUpdateConcurrencyException)
            {
                result.Message = "No data found";
            }

            return result;
        }
        public async Task<ReturnStatus> Add(CmmProductFee ProductFee)
        {
            ReturnStatus result = new();
            try
            {
                _context.CmmProductFees.Add(ProductFee);
                await _context.SaveChangesAsync();
                result.Success = true;
                result.Message = "Successfull! Data was successfully added";
            }
            catch (DbUpdateConcurrencyException)
            {
                result.Message = "No data found";
            }
            return result;
        }
        public async Task<ReturnStatus> Delete(int FeeId)
        {
            ReturnStatus result = new() { Message = "No data found" };
            try
            {
                var user = await _context.CmmProductFees.FindAsync(FeeId);
                if (user != null)
                {
                    _context.CmmProductFees.Remove(user);
                    await _context.SaveChangesAsync();
                    result.Success = true;
                    result.Message = "Successfull! Data was successfully deleted";
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                result.Message = "No data found";
            }
            return result;
        }
    }
}
