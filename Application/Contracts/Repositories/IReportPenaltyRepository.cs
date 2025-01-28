using Application.Models.Structs;
using System.Data;

namespace Application.Contracts.Repositories
{
    public interface IReportPenaltyRepository
    {
        Task<DataTable> GetDetailedPenaltyItems(Client details);
    }
}
