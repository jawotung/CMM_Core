using Application.Models;
using Application.Models.Responses;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Application.Contracts.Data
{
    public interface ISQLDataAccess
    {
        Task<DataTable> LoadDataTable<U>(string storedProcedure, U parameters, string connectionId = "CMMDBContext");
        Task<IEnumerable<T>> LoadList<T, U>(string storedProcedure, U parameters, string connectionId = "CMMDBContext");
        Task<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionId = "CMMDBContext");
        Task ExecuteQuery<T>(string storedProcedure, T parameters, string connectionId = "CMMDBContext");
        Task<ReturnStatus> SaveData<T>(string storedProcedure, T parameters, string connectionId = "CMMDBContext");
        Task<(IEnumerable<T1>, int)> LoadPaginationList<T1, T2, U>(string storedProcedure, U parameters, string connectionId = "CMMDBContext");
        Task<string> LoadSingle<T, U>(string storedProcedure, U parameters, string connectionId = "CMMDBContext");
    }
}
