namespace AMDT.API.Models.DTOs
{
    public class StatusDto
    {
        public int StatusID { get; set; }
        public string StatusName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
