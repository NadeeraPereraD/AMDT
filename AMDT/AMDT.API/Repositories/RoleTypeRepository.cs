using AMDT.API.Data;
using AMDT.API.Interfaces;
using AMDT.API.Models.DTOs;
using AMDT.API.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AMDT.API.Repositories
{
    public class RoleTypeRepository : IRoleTypeRepository
    {
        private readonly AppDbContext _context;

        public RoleTypeRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> CreateAsync(RoleTypeCreateDto dto)
        {
            try
            {
                var conn = _context.Database.GetDbConnection();
                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                await using var cmd = conn.CreateCommand();
                cmd.CommandText = "usp_RoleType_Create";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@RoleName", dto.RoleName));
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
        public async Task<(IEnumerable<RoleTypeDto> roleTypes, string? ErrorMessage, string? SuccessMessage)> GetAllAsync()
        {
            try
            {
                var errorParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };
                var successParam = new SqlParameter("@SuccessMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };

                var roleTypes = await _context.Set<RoleTypeDto>()
                    .FromSqlRaw("EXEC dbo.usp_RoleType_GetAll @ErrorMessage OUTPUT, @SuccessMessage OUTPUT",
                                 errorParam, successParam)
                    .ToListAsync();

                var errorMsg = errorParam.Value as string;
                var successMsg = successParam.Value as string;

                return (roleTypes, errorMsg, successMsg);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> UpdateByKeyAsync(RoleTypeUpdateDto dto)
        {
            try
            {
                var conn = _context.Database.GetDbConnection();
                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                await using var cmd = conn.CreateCommand();
                cmd.CommandText = "usp_RoleType_UpdateByID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@RoleID", dto.RoleID));
                cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@RoleName", dto.RoleName));
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
        public async Task<(bool IsSuccess, string? ErrorMessage, String? SuccessMessage)> DeleteByKeyAsync(int RoleID)
        {
            try
            {
                var conn = _context.Database.GetDbConnection();
                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                await using var cmd = conn.CreateCommand();
                cmd.CommandText = "usp_RoleType_DeleteByID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@RoleID", RoleID));

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
