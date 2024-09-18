namespace WorkManagerSystemBackend.Core.Dtos.Space
{
    public class SpaceCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long UsersId { get; set; }
    }
}
