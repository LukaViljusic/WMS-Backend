namespace WorkManagerSystemBackend.Core.Dtos.WorkItemState
{
    public class WorkItemStateGetDto
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String Color { get; set; }
        public String InitialStatus { get; set; }
        public String FinalStatus { get; set; }
        public long SpaceId { get; set; }
        public String SpaceName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
