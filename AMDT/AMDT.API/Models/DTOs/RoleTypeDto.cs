namespace AMDT.API.Models.DTOs
{
    public class RoleTypeDto
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
