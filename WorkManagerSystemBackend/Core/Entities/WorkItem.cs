using Microsoft.VisualBasic;

namespace WorkManagerSystemBackend.Core.Entities
{
    public class WorkItem : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Deadline { get; set; }
        public int TimeEstimate { get; set; }

        //public long TypeId { get; set; } = 0;//Promjena treba se pobrisat workItemTypeId ili TypeId doslo je do greske negdje

        //Relations
        public long SpaceId { get; set; }
        public Space Space { get; set; }
        public long WorkItemTypeId { get; set; }
        public WorkItemType WorkItemType { get; set; }
        public long WorkItemStateId { get; set; }
        public WorkItemState WorkItemState { get; set; }
        public long UsersId { get; set; }
        public Users Users { get; set; }

    }
}
