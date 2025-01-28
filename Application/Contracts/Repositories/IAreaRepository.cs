using Application.Models.DTOs.Area;
using Application.Models.Helpers;
using Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI;

namespace Application.Contracts.Repositories
{
    public interface IAreaRepository
    {
        Task<PaginatedList<CmmArea, AreaDTO>> GetAreaList(int page = 1, string search = "");
        Task<List<CmmArea>> GetAreaList();
        Task<CmmArea> GetArea(string AreaCode);
        Task<CmmArea> GetArea(int AreaID);
        Task<ReturnStatus> Update(CmmArea area);
        Task<ReturnStatus> Add(CmmArea area);
        Task<ReturnStatus> Delete(int AreaID);
    }
}
