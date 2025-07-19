using AMDT.API.Models.DTOs;
using AMDT.API.Models.Entities;

namespace AMDT.API.Interfaces
{
    public interface IUserDetailsService
    {
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> CreateAsync(UserDetailsCreateDto dto);
        Task<(IEnumerable<UserDetail> userDetails, string? ErrorMessage, string? SuccessMessage)> GetAllUserDetailsAsync();
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> UpdateAsyncByID(UserDetailsUpdateDto dto);
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> DeleteAsyncByID(UserDetailsRequestDto request);
        Task<(bool IsSuccess, string? ErrorMessage, string? Token)> LoginAsync(LoginRequestDto dto);
    }
}
