using System.Text.Json.Serialization;

namespace WorkManagerSystemBackend.Core.Entities
{
    public class Users : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [JsonIgnore] public string Password { get; set; }
        // Relations
        public ICollection<Space> Spaces { get; set; }
        public ICollection<WorkItem> WorkItems { get; set; }

        //izmjena
        public ICollection<UserSpace> UserSpaces { get; set; }
    }
}
