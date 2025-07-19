using AMDT.API.Data;
using AMDT.API.Interfaces;
using AMDT.API.Models.DTOs;
using AMDT.API.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AMDT.API.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly AppDbContext _context;

        public StatusRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> CreateAsync(StatusCreateDto dto)
        {
            try
            {
                var conn = _context.Database.GetDbConnection();
                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                await using var cmd = conn.CreateCommand();
                cmd.CommandText = "usp_Status_Create";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@StatusName", dto.StatusName));

                var pError = new Microsoft.Data.SqlClient.SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 500)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(pError);

                var pSuccess = new Microsoft.Data.SqlClient.SqlParameter("@SuccessMessage", SqlDbType.NVarChar, 500)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(pSuccess);
                await cmd.ExecuteNonQueryAsync();
                var errorMsg = pError.Value as string;
                var successMsg = pSuccess.Value as string;
                var isSuccess = string.IsNullOrEmpty(errorMsg);

                return (isSuccess, errorMsg, successMsg);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<(IEnumerable<Status> statuses, string? ErrorMessage, string? SuccessMessage)> GetAllAsync()
        {
            try
            {
                var errorParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };
                var successParam = new SqlParameter("@SuccessMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };

                var status = await _context.Statuses
                    .FromSqlRaw("EXEC dbo.usp_Status_GetAll @ErrorMessage OUTPUT, @SuccessMessage OUTPUT",
                                 errorParam, successParam)
                    .ToListAsync();

                var errorMsg = errorParam.Value as string;
                var successMsg = successParam.Value as string;

                return (status, errorMsg, successMsg);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> UpdateByKeyAsync(StatusUpdateDto dto)
        {
            try
            {
                var conn = _context.Database.GetDbConnection();
                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                await using var cmd = conn.CreateCommand();
                cmd.CommandText = "usp_Status_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@StatusName", dto.StatusName));

                var pError = new Microsoft.Data.SqlClient.SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 500)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(pError);

                var pSuccess = new Microsoft.Data.SqlClient.SqlParameter("@SuccessMessage", SqlDbType.NVarChar, 500)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(pSuccess);
                await cmd.ExecuteNonQueryAsync();
                var errorMsg = pError.Value as string;
                var successMsg = pSuccess.Value as string;
                var isSuccess = string.IsNullOrEmpty(errorMsg);

                return (isSuccess, errorMsg, successMsg);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<(bool IsSuccess, string? ErrorMessage, String? SuccessMessage)> DeleteByKeyAsync(int StatusID)
        {
            try
            {
                var conn = _context.Database.GetDbConnection();
                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                await using var cmd = conn.CreateCommand();
                cmd.CommandText = "usp_Status_Update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@StatusID", StatusID));

                var pError = new Microsoft.Data.SqlClient.SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 500)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(pError);

                var pSuccess = new Microsoft.Data.SqlClient.SqlParameter("@SuccessMessage", SqlDbType.NVarChar, 500)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(pSuccess);
                await cmd.ExecuteNonQueryAsync();
                var errorMsg = pError.Value as string;
                var successMsg = pSuccess.Value as string;
                var isSuccess = string.IsNullOrEmpty(errorMsg);

                return (isSuccess, errorMsg, successMsg);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
