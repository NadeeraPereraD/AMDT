using AMDT.API.Data;
using AMDT.API.Interfaces;
using AMDT.API.Models.DTOs;
using AMDT.API.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AMDT.API.Repositories
{
    public class UserDetailsRepository : IUserDetailsRepository
    {
        private readonly AppDbContext _context;

        public UserDetailsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> CreateAsync(UserDetailsCreateDto dto)
        {
            try
            {
                var conn = _context.Database.GetDbConnection();
                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                await using var cmd = conn.CreateCommand();
                cmd.CommandText = "usp_UserDetails_Create";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@FirstName", dto.FirstName));
                cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@LastName", dto.LastName));
                cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@Email", dto.Email));
                cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@Password", dto.Password));
                cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@DateOfBirth", dto.DateOfBirth));
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
        public async Task<(IEnumerable<UserDetailsDto> userDetails, string? ErrorMessage, string? SuccessMessage)> GetAllAsync()
        {
            try
            {
                var errorParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };
                var successParam = new SqlParameter("@SuccessMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };

                var users = await _context.Set<UserDetailsDto>()
                    .FromSqlRaw("EXEC dbo.usp_UserDetails_GetAll @ErrorMessage OUTPUT, @SuccessMessage OUTPUT",
                                 errorParam, successParam)
                    .ToListAsync();

                var errorMsg = errorParam.Value as string;
                var successMsg = successParam.Value as string;

                return (users, errorMsg, successMsg);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> UpdateByKeyAsync(UserDetailsUpdateDto dto)
        {
            try
            {
                var conn = _context.Database.GetDbConnection();
                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                await using var cmd = conn.CreateCommand();
                cmd.CommandText = "usp_UserDetails_UpdateByID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@UserID", dto.UserID));
                cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@FirstName", dto.FirstName));
                cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@LastName", dto.LastName));
                cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@Email", dto.Email));
                cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@DateOfBirth", dto.DateOfBirth));
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
        public async Task<(bool IsSuccess, string? ErrorMessage, String? SuccessMessage)> DeleteByKeyAsync(int UserID)
        {
            try
            {
                var conn = _context.Database.GetDbConnection();
                if (conn.State != ConnectionState.Open)
                    await conn.OpenAsync();

                await using var cmd = conn.CreateCommand();
                cmd.CommandText = "usp_UserDetails_DeleteByID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@UserID", UserID));

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
        public async Task<UserDetail?> GetUserByEmailAsync(string email)
        {
            return await _context.UserDetails
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
