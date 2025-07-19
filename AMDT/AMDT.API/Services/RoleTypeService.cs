using AMDT.API.Interfaces;
using AMDT.API.Models.DTOs;
using AMDT.API.Models.Entities;
using AutoMapper;

namespace AMDT.API.Services
{
    public class RoleTypeService : IRoleTypeService
    {
        private readonly IRoleTypeRepository _repository;
        private readonly IMapper _mapper;

        public RoleTypeService(IRoleTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> CreateAsync(RoleTypeCreateDto dto)
            => await _repository.CreateAsync(dto);
        public Task<(IEnumerable<RoleType> roleTypes, string? ErrorMessage, string? SuccessMessage)> GetAllRoleTypeAsync()
            => _repository.GetAllAsync();
        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> UpdateAsyncByID(RoleTypeUpdateDto dto)
            => await _repository.UpdateByKeyAsync(dto);
        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> DeleteAsyncByID(RoleTypeRequestDto request)
            => await _repository.DeleteByKeyAsync(request.RoleID);
    }
}
