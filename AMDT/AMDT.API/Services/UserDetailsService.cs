using AMDT.API.Interfaces;
using AMDT.API.Models.DTOs;
using AMDT.API.Models.Entities;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;


namespace AMDT.API.Services
{
    public class UserDetailsService : IUserDetailsService
    {
        private readonly IUserDetailsRepository _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserDetailsService(IUserDetailsRepository repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> CreateAsync(UserDetailsCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || !IsValidEmail(dto.Email))
                return (false, "Invalid email format.", null);

            if (!IsValidPassword(dto.Password))
                return (false, "Password must be 8–12 characters and contain at least one uppercase letter, one lowercase letter, and one special character.", null);

            dto.Password = HashPassword(dto.Password);

            return await _repository.CreateAsync(dto);
        }
        public Task<(IEnumerable<UserDetailsDto> userDetails, string? ErrorMessage, string? SuccessMessage)> GetAllUserDetailsAsync()
            => _repository.GetAllAsync();
        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> UpdateAsyncByID(UserDetailsUpdateDto dto)
            => await _repository.UpdateByKeyAsync(dto);
        public async Task<(bool IsSuccess, string? ErrorMessage, string? SuccessMessage)> DeleteAsyncByID(UserDetailsRequestDto request)
            => await _repository.DeleteByKeyAsync(request.UserID);

        public async Task<(bool IsSuccess, string? ErrorMessage, string? Token)> LoginAsync(LoginRequestDto dto)
        {
            var user = await _repository.GetUserByEmailAsync(dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                return (false, "Invalid email or password", null);

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtKey = _configuration["Jwt:Key"];
            if (string.IsNullOrWhiteSpace(jwtKey))
                throw new InvalidOperationException("JWT Key is missing in configuration.");

            var key = Encoding.UTF8.GetBytes(jwtKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RoleType.ToString())
            }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);

            return (true, null, tokenString);
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase);
        }

        //private string HashPassword(string password)
        //{
        //    using var rng = RandomNumberGenerator.Create();
        //    byte[] salt = new byte[16];
        //    rng.GetBytes(salt);

        //    var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
        //    byte[] hash = pbkdf2.GetBytes(32); 

        //    var result = new byte[48]; 
        //    Array.Copy(salt, 0, result, 0, 16);
        //    Array.Copy(hash, 0, result, 16, 32);

        //    return Convert.ToBase64String(result);
        //}

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;

            if (password.Length < 8 || password.Length > 12) return false;

            bool hasUpper = password.Any(char.IsUpper);
            bool hasLower = password.Any(char.IsLower);
            bool hasSpecial = password.Any(ch => !char.IsLetterOrDigit(ch));

            return hasUpper && hasLower && hasSpecial;
        }


    }
}
