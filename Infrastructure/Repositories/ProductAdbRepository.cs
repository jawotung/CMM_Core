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
    public class ProductAdbRepository : IProductAdbRepository
    {
        private readonly CMMDBContext _context;
        private readonly IMapper _mapper;
        public ProductAdbRepository(CMMDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<CmmProductAdb, ProductAdbDTO>> GetProductAdbList(int ProductId, int page = 1, string search = "")
        {
            IQueryable<CmmProductAdb> list = _context.CmmProductAdbs
                                        .Where(x => x.ProductId == ProductId)
                                        .Where(x => string.IsNullOrEmpty(search) ||
                                                    x.AdbName.Contains(search) ||
                                                    x.AdbAmount.Contains(search))
                                        .OrderBy(x => x.AdbName);
            return await PaginatedList<CmmProductAdb, ProductAdbDTO>.CreateAsync(list, _mapper, page);
        }
        public async Task<List<CmmProductAdb>> GetProductAdbList()
        {
            return await _context.CmmProductAdbs.ToListAsync();
        }
        public async Task<CmmProductAdb> GetProductAdb(int ProductId, string AdbName)
        {
            return await _context.CmmProductAdbs.Where(x => x.AdbName == AdbName && x.ProductId == ProductId).FirstOrDefaultAsync() ?? new();
        }
        public async Task<CmmProductAdb> GetProductAdb(int ProductAdbId)
        {
            return await _context.CmmProductAdbs.FindAsync(ProductAdbId) ?? new();
        }
        public async Task<ReturnStatus> Update(CmmProductAdb productAdb)
        {
            ReturnStatus result = new();
            try
            {
                var existing = await _context.CmmProductAdbs.FindAsync(productAdb.ProductAdbId);
                if (existing == null)
                {
                    result.Message = "No data found";
                    return result;
                }

                _mapper.Map(productAdb, existing);
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
        public async Task<ReturnStatus> Add(CmmProductAdb productAdb)
        {
            ReturnStatus result = new();
            try
            {
                _context.CmmProductAdbs.Add(productAdb);
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
        public async Task<ReturnStatus> Delete(int ProductAdbId)
        {
            ReturnStatus result = new() { Message = "No data found" };
            try
            {
                var user = await _context.CmmProductAdbs.FindAsync(ProductAdbId);
                if (user != null)
                {
                    _context.CmmProductAdbs.Remove(user);
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
