using Application.Contracts.Repositories;
using Application.Contracts.Services;
using Application.Models.DTOs.Area;
using Application.Models.Helpers;
using Application.Models.Responses;
using AutoMapper;
using WebAPI;

namespace Application.Services
{
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _repo;
        private readonly IMapper _mapper;
        public AreaService(IAreaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<PaginatedList<AreaDTO>> GetAreaList(int Page = 1,string Search = "")
        {
            PaginatedList<CmmArea, AreaDTO> data = await _repo.GetAreaList(Page, Search);
            return new PaginatedList<AreaDTO>(data.Data, data.PageIndex, data.TotalPages, data.CountData);
        }
        public async Task<List<AreaDTO>> GetAreaList()
        {
            List<CmmArea> data = await _repo.GetAreaList();
            return _mapper.Map<List<AreaDTO>>(data);
        }
        public async Task<ReturnStatus> SaveArea(AreaDTO area)
        {
            ReturnStatus result = new();
            try
            {
                CmmArea data = _mapper.Map<CmmArea>(area);
                if (await AreaCodeExists(data.AreaId, data.AreaCode))
                {
                    result.Message = "Area Code already existing";
                    return result;
                }
                if (await AreaNameExists(data.AreaId, data.AreaName))
                {
                    result.Message = "Area Name already existing";
                    return result;
                }
                if (area.AreaId == 0)
                    result = await _repo.Add(data);
                else
                {
                    var existing = await _repo.GetArea(area.AreaId);
                    if (existing.AreaId == 0)
                    {
                        result.Message = "No daa found";
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
        public async Task<ReturnStatus> DeleteArea(int id)
        {
            ReturnStatus result = new();
            try
            {
                var area = await _repo.GetArea(id);
                if (area.AreaId == 0)
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
        private async Task<bool> AreaCodeExists(int AreaId, string AreaCode)
        {
            var data = await _repo.GetArea(AreaCode);
            return data != null && (AreaId != 0 ? (data.AreaId != AreaId && data.AreaCode == AreaCode) : data.AreaCode == AreaCode);
        }
        private async Task<bool> AreaNameExists(int AreaId, string AreaName)
        {
            var data = await _repo.GetArea(AreaName);
            return data != null && (AreaId != 0 ? (data.AreaId != AreaId && data.AreaName == AreaName) : data.AreaName == AreaName);
        }
    } 

}
