namespace WorkManagerSystemBackend.Core.Dtos.WorkItemType
{
    public class WorkItemTypeGetDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public long SpaceId { get; set; }
        public String SpaceName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
