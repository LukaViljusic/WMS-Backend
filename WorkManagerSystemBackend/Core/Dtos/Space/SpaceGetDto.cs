namespace WorkManagerSystemBackend.Core.Dtos.Space
{
    public class SpaceGetDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long UsersId { get; set; }
        public String UserName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
