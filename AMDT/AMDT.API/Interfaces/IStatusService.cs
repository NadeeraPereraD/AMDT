using AMDT.API.Models.DTOs;
using AMDT.API.Models.Entities;

namespace AMDT.API.Interfaces
{
    public interface IStatusService
    {
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> CreateAsync(StatusCreateDto dto);
        Task<(IEnumerable<Status> statuses, string? ErrorMessage, string? SuccessMessage)> GetAllStatusAsync();
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> UpdateAsyncByID(StatusUpdateDto dto);
        Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> DeleteAsyncByID(StatusRequestDto request);
    }
}
