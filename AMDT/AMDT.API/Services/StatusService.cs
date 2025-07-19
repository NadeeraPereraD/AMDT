using AMDT.API.Interfaces;
using AMDT.API.Models.DTOs;
using AMDT.API.Models.Entities;
using AutoMapper;

namespace AMDT.API.Services
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _repository;
        private readonly IMapper _mapper;

        public StatusService(IStatusRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> CreateAsync(StatusCreateDto dto)
            => await _repository.CreateAsync(dto);

        public Task<(IEnumerable<Status> statuses, string? ErrorMessage, string? SuccessMessage)> GetAllStatusAsync()
            => _repository.GetAllAsync();

        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> UpdateAsyncByID(StatusUpdateDto dto)
            => await _repository.UpdateByKeyAsync(dto);

        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> DeleteAsyncByID(StatusRequestDto request)
            => await _repository.DeleteByKeyAsync(request.StatusID);

    }
}
