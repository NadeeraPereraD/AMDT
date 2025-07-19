using AMDT.API.Models.DTOs;
using AMDT.API.Models.Entities;

namespace AMDT.API.Interfaces
{
    public interface IUserDetailsRepository
    {
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> CreateAsync(UserDetailsCreateDto dto);
        Task<(IEnumerable<UserDetail> userDetails, string? ErrorMessage, string? SuccessMessage)> GetAllAsync();
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> UpdateByKeyAsync(UserDetailsUpdateDto dto);
        Task<(bool IsSuccess, string? ErrorMessage, String? SuccessMessage)> DeleteByKeyAsync(int UserID);
        Task<UserDetail?> GetUserByEmailAsync(string email);
    }
}
