using System.Drawing;

namespace WorkManagerSystemBackend.Core.Entities
{
    public class WorkItemState : BaseEntity
    {
        public String Name {  get; set; }
        public String Color { get; set; }
        public String InitialStatus { get; set; }
        public String FinalStatus { get; set; }
        //Relations
        /*public long WorkItemTypeId { get; set; }
        public WorkItemType WorkItemType { get; set; }*/

        public long SpaceId { get; set; }
        public Space Space { get; set; }
        public ICollection<WorkItem> WorkItems { get; set; }

    }
}
