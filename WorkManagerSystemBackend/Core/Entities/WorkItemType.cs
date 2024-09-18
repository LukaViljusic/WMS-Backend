using System.Drawing;

namespace WorkManagerSystemBackend.Core.Entities
{
    public class WorkItemType : BaseEntity
    {
        public string Name {  get; set; }
        public string Color { get; set; }

        //Relations
        public long SpaceId { get; set; }
        public Space Space { get; set; }
        public ICollection<WorkItem> WorkItems { get; set; }
    }
}
