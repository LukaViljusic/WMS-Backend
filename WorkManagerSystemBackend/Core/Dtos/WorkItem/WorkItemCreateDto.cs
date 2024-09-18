using Microsoft.VisualBasic;

namespace WorkManagerSystemBackend.Core.Dtos.WorkItem
{
    public class WorkItemCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Deadline { get; set; }
        public int TimeEstimate { get; set; }
        public long UsersId { get; set; }
        public long SpaceId { get; set; }
        public long WorkItemTypeId { get; set; }
        public long WorkItemStateId { get; set; }

    }
}
