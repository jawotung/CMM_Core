using Application.Models.DTOs.Area;
using Application.Models.Helpers;
using Application.Models.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI;

namespace Infrastructure.Repositories
{
    public class ProductRepository
    {
        private readonly CMMDBContext _context;
        private readonly IMapper _mapper;
        public ProductRepository(CMMDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<CmmProduct, AreaDTO>> GetProductList(int page = 1, string search = "")
        {
            IQueryable<CmmProduct> list = _context.CmmProducts
                                        .Where(x => string.IsNullOrEmpty(search) ||
                                                    x.ProductCode.Contains(search) ||
                                                    x.ProductName.Contains(search))
                                        .OrderBy(x => x.ProductCode);
            return await PaginatedList<CmmProduct, AreaDTO>.CreateAsync(list, _mapper, page);
        }
        public async Task<List<CmmProduct>> GetProductList()
        {
            return await _context.CmmProducts.ToListAsync();
        }
        public async Task<CmmProduct> GetProduct(string ProductCode)
        {
            return await _context.CmmProducts.Where(x => x.ProductCode == ProductCode).FirstOrDefaultAsync() ?? new();
        }
        public async Task<CmmProduct> GetProduct(int ProductId)
        {
            return await _context.CmmProducts.FindAsync(ProductId) ?? new();
        }
        public async Task<ReturnStatus> Update(CmmProduct product)
        {
            ReturnStatus result = new();
            try
            {
                var existing = await _context.CmmProducts.FindAsync(product.ProductId);
                if (existing == null)
                {
                    result.Message = "No data found";
                    return result;
                }

                _mapper.Map(product, existing);
                existing.DateCreated = DateTime.Now;
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
        public async Task<ReturnStatus> Add(CmmProduct product)
        {
            ReturnStatus result = new();
            try
            {
                _context.CmmProducts.Add(product);
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
        public async Task<ReturnStatus> Delete(int ProductId)
        {
            ReturnStatus result = new() { Message = "No data found" };
            try
            {
                var user = await _context.CmmProducts.FindAsync(ProductId);
                if (user != null)
                {
                    _context.CmmProducts.Remove(user);
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
