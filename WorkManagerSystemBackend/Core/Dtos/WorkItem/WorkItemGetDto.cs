using Microsoft.VisualBasic;

namespace WorkManagerSystemBackend.Core.Dtos.WorkItem
{
    public class WorkItemGetDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Deadline { get; set; }
        public int TimeEstimate { get; set; }
        public long UsersId { get; set; }
        public String UserName { get; set; }
        public String FinalStatus { get; set; }
        public long SpaceId { get; set; }
        public String SpaceName { get; set; }
        public long WorkItemTypeId { get; set; }
        public String WorkItemTypeName { get; set; }
        public String WorkItemTypeColor { get; set; }
        public long WorkItemStateId { get; set; }
        public String WorkItemStateColor { get; set; }
        public String WorkItemStateName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
