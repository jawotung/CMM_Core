using Application.Contracts.Repositories;
using Application.Models.DTOs.Product;
using Application.Models.Helpers;
using Application.Models.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI;

namespace Infrastructure.Repositories
{
    public class ProductTierRepository : IProductTierRepository
    {
        private readonly CMMDBContext _context;
        private readonly IMapper _mapper;
        public ProductTierRepository(CMMDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<CmmProductTier, ProductTierDTO>> GetProductTierList(int ProductAdbId, int page = 1, string search = "")
        {
            IQueryable<CmmProductTier> list = _context.CmmProductTiers
                                        .Where(x => x.ProductAdbId == ProductAdbId)
                                        .Where(x => string.IsNullOrEmpty(search) ||
                                                    x.TierName.Contains(search) ||
                                                    x.TierDesc.Contains(search))
                                        .OrderBy(x => x.TierName);
            return await PaginatedList<CmmProductTier, ProductTierDTO>.CreateAsync(list, _mapper, page);
        }
        public async Task<List<CmmProductTier>> GetProductTierList()
        {
            return await _context.CmmProductTiers.ToListAsync();
        }
        public async Task<CmmProductTier> GetProductTier(int ProductAdbId, string TierName)
        {
            return await _context.CmmProductTiers.Where(x => x.TierName == TierName && x.ProductAdbId == ProductAdbId).FirstOrDefaultAsync() ?? new();
        }
        public async Task<CmmProductTier> GetProductTierByDesc(int ProductAdbId, string TierDesc)
        {
            return await _context.CmmProductTiers.Where(x => x.TierDesc == TierDesc && x.ProductAdbId == ProductAdbId).FirstOrDefaultAsync() ?? new();
        }
        public async Task<CmmProductTier> GetProductTier(string TierDesc, int ProductAdbId)
        {
            return await _context.CmmProductTiers.Where(x => x.TierDesc == TierDesc && x.ProductAdbId == ProductAdbId).FirstOrDefaultAsync() ?? new();
        }
        public async Task<CmmProductTier> GetProductTier(int FeeId)
        {
            return await _context.CmmProductTiers.FindAsync(FeeId) ?? new();
        }
        public async Task<ReturnStatus> Update(CmmProductTier ProductTier)
        {
            ReturnStatus result = new();
            try
            {
                var existing = await _context.CmmProductTiers.FindAsync(ProductTier.TierId);
                if (existing == null)
                {
                    result.Message = "No data found";
                    return result;
                }

                _mapper.Map(ProductTier, existing);
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
        public async Task<ReturnStatus> Add(CmmProductTier ProductTier)
        {
            ReturnStatus result = new();
            try
            {
                _context.CmmProductTiers.Add(ProductTier);
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
                var user = await _context.CmmProductTiers.FindAsync(FeeId);
                if (user != null)
                {
                    _context.CmmProductTiers.Remove(user);
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
