using AMDT.API.Exceptions;
using AMDT.API.Interfaces;
using AMDT.API.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMDT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StatusCreateDto dto)
        {
            try
            {
                var result = await _statusService.CreateAsync(dto);
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
                var (statuses, error, success) = await _statusService.GetAllStatusAsync();
                var list = statuses;

                if (!string.IsNullOrEmpty(error) && list is not { } || list.Any() is false)
                    return NotFound(new { Message = error });

                return Ok(new
                {
                    Message = success,
                    Data = statuses
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
        public async Task<IActionResult> UpdateByKey([FromBody] StatusUpdateDto dto)
        {
            try
            {
                if (dto == null || dto.StatusID <= 0)
                    return BadRequest(new { message = "ID is required in the request body." });
                var result = await _statusService.UpdateAsyncByID(dto);
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
        public async Task<IActionResult> DeleteByKey([FromBody] StatusRequestDto dto)
        {
            try
            {
                if (dto == null || dto.StatusID <= 0)
                    return BadRequest(new { message = "ID is required in the request body." });

                var result = await _statusService.DeleteAsyncByID(dto);

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
