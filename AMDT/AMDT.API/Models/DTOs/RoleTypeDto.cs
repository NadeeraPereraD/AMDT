namespace AMDT.API.Models.DTOs
{
    public class RoleTypeDto
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; } = null!;
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
