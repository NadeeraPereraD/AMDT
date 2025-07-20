namespace AMDT.API.Models.DTOs
{
    public class UserDetailsDto
    {
        public int UserID { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        //public string Password { get; set; } = null!;
        public string DateOfBirth { get; set; } = null!;
        public string RoleType { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
