using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Application.Models;
using System.Runtime.InteropServices;
using Application.Contracts.Data;
using Application.Models.Responses;
using FastMember;

namespace Infrastructure.DbAccess
{
    public class SQLDataAccess : ISQLDataAccess
    {
        private readonly IConfiguration _config;
        public SQLDataAccess(IConfiguration config)
        {
            _config = config;
        }
        public async Task<DataTable> LoadDataTable<U>(string storedProcedure, U parameters, string connectionId = "CMMDBContext")
        {
            using (IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId)))
            {
                var result = await connection.QueryAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

                // Use FastMember to create a DataTable
                var dataTable = new DataTable();
                using (var reader = ObjectReader.Create(result))
                {
                    dataTable.Load(reader);
                }

                return dataTable;
            }
        }
        public async Task<IEnumerable<T>> LoadList<T, U>(string storedProcedure, U parameters, string connectionId = "CMMDBContext")
        {
            using (IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId)))
            {
                return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionId = "CMMDBContext")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            return await connection.QueryFirstOrDefaultAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }
        public async Task ExecuteQuery<T>(string storedProcedure, T parameters, string connectionId = "CMMDBContext")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }
        public async Task<ReturnStatus> SaveData<T>(string storedProcedure, T parameters, string connectionId = "CMMDBContext")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            var dynamicParameters = new DynamicParameters(parameters);

            dynamicParameters.Add("@StatusCode", dbType: DbType.String, size: 2, direction: ParameterDirection.Output);
            dynamicParameters.Add("@StatusMessage", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);

            // Execute the stored procedure
            await connection.ExecuteAsync(storedProcedure, dynamicParameters, commandType: CommandType.StoredProcedure);

            // Retrieve the output parameter value after execution
            string StatusCode = dynamicParameters.Get<string>("@StatusCode");
            string StatusMessage = dynamicParameters.Get<string>("@StatusMessage");
            return new ReturnStatus { Success = true, Message = StatusMessage };
        }
        public async Task<(IEnumerable<T1>, int)> LoadPaginationList<T1, T2, U>(string storedProcedure, U parameters, string connectionId = "CMMDBContext")
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

                using var multi = await connection.QueryMultipleAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

                var firstResult = await multi.ReadAsync<T1>();

                var secondResult = await multi.ReadSingleAsync<int>(); // ReadSingleAsync<T> for other types

                return (firstResult, secondResult);
            }
            catch
            {
                throw;
            }
        }
        public async Task<string> LoadSingle<T, U>(string storedProcedure, U parameters, string connectionId = "CMMDBContext")
        {
            try
            {
                using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

                var multi = await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                var result = multi.FirstOrDefault();

                // Assuming T has a property called 'Name'
                if (result != null)
                {
                    if (result is IDictionary<string, object> dictionary)
                    {
                        // Get the first value from the dictionary
                        var firstValue = dictionary.Values.FirstOrDefault();
                        return firstValue?.ToString() ?? "";
                    }
                }
                return "";
            }
            catch
            {
                throw;
            }
        }
    }
}
