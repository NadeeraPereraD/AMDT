namespace AMDT.API.Models.DTOs
{
    public class RoleTypeUpdateDto
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; } = null!;
        public string StatusName { get; set; } = null!;

    }
}
