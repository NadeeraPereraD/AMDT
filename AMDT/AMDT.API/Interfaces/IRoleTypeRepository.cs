using AMDT.API.Models.DTOs;
using AMDT.API.Models.Entities;

namespace AMDT.API.Interfaces
{
    public interface IRoleTypeRepository
    {
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> CreateAsync(RoleTypeCreateDto dto);
        Task<(IEnumerable<RoleType> roleTypes, string? ErrorMessage, string? SuccessMessage)> GetAllAsync();
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> UpdateByKeyAsync(RoleTypeUpdateDto dto);
        Task<(bool IsSuccess, string? ErrorMessage, String? SuccessMessage)> DeleteByKeyAsync(int RoleID);
    }
}
