namespace WorkManagerSystemBackend.Core.Entities
{
    public class Space : BaseEntity
    {
        public string Name {  get; set; }
        public string Description { get; set; }

        //Relations
        public long UsersId { get; set; }
        public Users Users { get; set; }

        public ICollection<WorkItemType> WorkItemTypes { get; set; }
        public ICollection<WorkItemState> WorkItemStates { get; set; }
        public ICollection<WorkItem> WorkItems { get; set; }
        public ICollection<UserSpace> UserSpaces { get; set; }
    }
}
