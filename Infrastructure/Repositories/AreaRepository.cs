using Application.Contracts.Repositories;
using Application.Models.DTOs.Area;
using Application.Models.Helpers;
using Application.Models.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI;

namespace Infrastructure.Repositories
{
    public class AreaRepository : IAreaRepository
    {
        private readonly CMMDBContext _context;
        private readonly IMapper _mapper;
        public AreaRepository(CMMDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<CmmArea, AreaDTO>> GetAreaList(int page = 1, string search = "")
        {
            IQueryable<CmmArea> list = _context.CmmAreas
                                        .Where(x => string.IsNullOrEmpty(search) ||
                                                    x.AreaCode.Contains(search) ||
                                                    x.AreaName.Contains(search))
                                        .OrderBy(x => x.AreaCode);
            return await PaginatedList<CmmArea, AreaDTO>.CreateAsync(list, _mapper, page);
        }
        public async Task<List<CmmArea>> GetAreaList()
        {
            return await _context.CmmAreas.ToListAsync();
        }
        public async Task<CmmArea> GetArea(string AreaCode)
        {
            return await _context.CmmAreas.Where(x => x.AreaCode == AreaCode).FirstOrDefaultAsync() ?? new();
        }
        public async Task<CmmArea> GetArea(int AreaID)
        {
            return await _context.CmmAreas.FindAsync(AreaID) ?? new();
        }
        public async Task<ReturnStatus> Update(CmmArea area)
        {
            ReturnStatus result = new();
            try
            {
                var existing = await _context.CmmAreas.FindAsync(area.AreaId);
                if (existing == null)
                {
                    result.Message = "No data found";
                    return result;
                }

                _mapper.Map(area, existing);
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
        public async Task<ReturnStatus> Add(CmmArea area)
        {
            ReturnStatus result = new();
            try
            {
                _context.CmmAreas.Add(area);
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
        public async Task<ReturnStatus> Delete(int AreaID)
        {
            ReturnStatus result = new() { Message = "No data found" };
            try
            {
                var user = await _context.CmmAreas.FindAsync(AreaID);
                if (user != null)
                {
                    _context.CmmAreas.Remove(user);
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
