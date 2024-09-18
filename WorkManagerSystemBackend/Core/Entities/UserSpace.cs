namespace WorkManagerSystemBackend.Core.Entities
{
    public class UserSpace : BaseEntity
    {
        public long UsersId { get; set; }
        public Users Users { get; set; }

        public long SpaceId { get; set; }
        public Space Space { get; set; }
    }
}
