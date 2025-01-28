using Application.Models.DTOs.Area;
using Application.Models.Helpers;
using Application.Models.Responses;

namespace Application.Contracts.Services
{
    public interface IAreaService
    {
        Task<PaginatedList<AreaDTO>> GetAreaList(int Page = 1, string Search = "");
        Task<List<AreaDTO>> GetAreaList();
        Task<ReturnStatus> SaveArea(AreaDTO area);
        Task<ReturnStatus> DeleteArea(int id);
    }
}
