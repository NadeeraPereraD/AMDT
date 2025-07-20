using AMDT.API.Models.DTOs;
using AMDT.API.Models.Entities;

namespace AMDT.API.Interfaces
{
    public interface IRoleTypeService
    {
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> CreateAsync(RoleTypeCreateDto dto);
        Task<(IEnumerable<RoleTypeDto> roleTypes, string? ErrorMessage, string? SuccessMessage)> GetAllRoleTypeAsync();
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> UpdateAsyncByID(RoleTypeUpdateDto dto);
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> DeleteAsyncByID(RoleTypeRequestDto request);
    }
}
