using AMDT.API.Exceptions;
using AMDT.API.Interfaces;
using AMDT.API.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMDT.API.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly IUserDetailsService _userDetailsService;

        public UserDetailsController(IUserDetailsService userDetailsService)
        {
            _userDetailsService = userDetailsService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDetailsCreateDto dto)
        {
            try
            {
                var result = await _userDetailsService.CreateAsync(dto);
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var (users, error, success) = await _userDetailsService.GetAllUserDetailsAsync();
                var list = users;

                if (!string.IsNullOrEmpty(error) && list is not { } || list.Any() is false)
                    return NotFound(new { Message = error });

                return Ok(new
                {
                    Message = success,
                    Data = users
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

        [Authorize]
        [HttpPut("by-keys")]
        public async Task<IActionResult> UpdateByKey([FromBody] UserDetailsUpdateDto dto)
        {
            try
            {
                if (dto == null || dto.UserID <= 0)
                    return BadRequest(new { message = "ID is required in the request body." });
                var result = await _userDetailsService.UpdateAsyncByID(dto);
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

        [Authorize]
        [HttpDelete("by-keys")]
        public async Task<IActionResult> DeleteByKey([FromBody] UserDetailsRequestDto dto)
        {
            try
            {
                if (dto == null || dto.UserID <= 0)
                    return BadRequest(new { message = "ID is required in the request body." });

                var result = await _userDetailsService.DeleteAsyncByID(dto);

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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            try
            {
                var result = await _userDetailsService.LoginAsync(dto);

                if (!result.IsSuccess)
                    return Unauthorized(new { message = result.ErrorMessage });

                return Ok(new { token = result.Token });
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
                return StatusCode(500, new { message = "Internal server error", details = ex.Message });
            }
        }
    }
}
