namespace AMDT.API.Models.DTOs
{
    public class UserDetailsCreateDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string DateOfBirth { get; set; } = null!;
        public string RoleName { get; set; } = null!;
        public string StatusName { get; set; } = null!;
    }
}
