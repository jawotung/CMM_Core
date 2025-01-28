using Application.Contracts.Repositories;
using Application.Models.DTOs.Product;
using Application.Models.Helpers;
using Application.Models.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI;

namespace Infrastructure.Repositories
{
    public class ProductUdfRepository : IProductUdfRepository
    {
        private readonly CMMDBContext _context;
        private readonly IMapper _mapper;
        public ProductUdfRepository(CMMDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<CmmUdfTemplate, ProductUdfDTO>> GetProductUdfList(int ProductId, int page = 1, string search = "")
        {
            IQueryable<CmmUdfTemplate> list = _context.CmmUdfTemplates
                                        .Where(x => x.ProductId == ProductId)
                                        .Where(x => string.IsNullOrEmpty(search) ||
                                                    x.UdfLabel.Contains(search) ||
                                                    x.UdfType.Contains(search))
                                        .OrderBy(x => x.UdfLabel);
            return await PaginatedList<CmmUdfTemplate, ProductUdfDTO>.CreateAsync(list, _mapper, page);
        }
        public async Task<List<CmmUdfTemplate>> GetProductUdfList()
        {
            return await _context.CmmUdfTemplates.ToListAsync();
        }
        public async Task<CmmUdfTemplate> GetProductUdf(int ProductId, string UdfLabel)
        {
            return await _context.CmmUdfTemplates.Where(x => x.UdfLabel == UdfLabel && x.ProductId == ProductId).FirstOrDefaultAsync() ?? new();
        }
        public async Task<CmmUdfTemplate> GetProductUdf(Guid UdfItemId)
        {
            return await _context.CmmUdfTemplates.FindAsync(UdfItemId) ?? new();
        }
        public async Task<ReturnStatus> Update(CmmUdfTemplate productUdf)
        {
            ReturnStatus result = new();
            try
            {
                var existing = await _context.CmmUdfTemplates.FindAsync(productUdf.UdfItemId);
                if (existing == null)
                {
                    result.Message = "No data found";
                    return result;
                }

                _mapper.Map(productUdf, existing);
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
        public async Task<ReturnStatus> Add(CmmUdfTemplate productUdf)
        {
            ReturnStatus result = new();
            try
            {
                _context.CmmUdfTemplates.Add(productUdf);
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
        public async Task<ReturnStatus> Delete(Guid UdfItemId)
        {
            ReturnStatus result = new() { Message = "No data found" };
            try
            {
                var user = await _context.CmmUdfTemplates.FindAsync(UdfItemId);
                if (user != null)
                {
                    _context.CmmUdfTemplates.Remove(user);
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
