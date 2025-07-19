using AMDT.API.Models.DTOs;
using AMDT.API.Models.Entities;

namespace AMDT.API.Interfaces
{
    public interface IStatusRepository
    {
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> CreateAsync(StatusCreateDto dto);
        Task<(IEnumerable<Status> statuses, string? ErrorMessage, string? SuccessMessage)> GetAllAsync();
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> UpdateByKeyAsync(StatusUpdateDto dto);
        Task<(bool IsSuccess, string? ErrorMessage, String? SuccessMessage)> DeleteByKeyAsync(int StatusID);
    }
}
