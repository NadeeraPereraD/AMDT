using AMDT.API.Exceptions;
using AMDT.API.Interfaces;
using AMDT.API.Models.DTOs;
using AMDT.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMDT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleTypeController : ControllerBase
    {
        private readonly IRoleTypeService _roleTypeService;

        public RoleTypeController(IRoleTypeService roleTypeService)
        {
            _roleTypeService = roleTypeService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleTypeCreateDto dto)
        {
            try
            {
                var result = await _roleTypeService.CreateAsync(dto);
                bool ok = result.IsSuccess;
                string? error = result.ErrorMessage;
                string? success = result.SuccessMessage;

                if (!ok) return BadRequest(new { error });
                return Ok(new { message = success });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (UnauthorizedAccessAppException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
            catch (ForbiddenAccessException)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal server error", Details = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var (roleTypes, error, success) = await _roleTypeService.GetAllRoleTypeAsync();
                var list = roleTypes;

                if (!string.IsNullOrEmpty(error) && list is not { } || list.Any() is false)
                    return NotFound(new { Message = error });

                return Ok(new
                {
                    Message = success,
                    Data = roleTypes
                });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (UnauthorizedAccessAppException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
            catch (ForbiddenAccessException)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal server error", Details = ex.Message });
            }
        }

        [HttpPut("by-keys")]
        public async Task<IActionResult> UpdateByKey([FromBody] RoleTypeUpdateDto dto)
        {
            try
            {
                if (dto == null || dto.RoleID <= 0)
                    return BadRequest(new { message = "ID is required in the request body." });
                var result = await _roleTypeService.UpdateAsyncByID(dto);
                bool ok = result.IsSuccess;
                string? error = result.ErrorMessage;
                string? success = result.SuccessMessage;
                if (!ok) return BadRequest(new { error });
                return Ok(new { message = success });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (UnauthorizedAccessAppException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
            catch (ForbiddenAccessException)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal server error", Details = ex.Message });
            }
        }

        [HttpDelete("by-keys")]
        public async Task<IActionResult> DeleteByKey([FromBody] RoleTypeRequestDto dto)
        {
            try
            {
                if (dto == null || dto.RoleID <= 0)
                    return BadRequest(new { message = "ID is required in the request body." });

                var result = await _roleTypeService.DeleteAsyncByID(dto);

                bool ok = result.IsSuccess;
                string? error = result.ErrorMessage;
                string? success = result.SuccessMessage;

                if (!ok) return BadRequest(new { error });

                return Ok(new { message = success });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (UnauthorizedAccessAppException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
            catch (ForbiddenAccessException)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal server error", Details = ex.Message });
            }
        }
    }
}
