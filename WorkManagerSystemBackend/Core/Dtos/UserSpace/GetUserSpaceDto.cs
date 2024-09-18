using WorkManagerSystemBackend.Core.Dtos.Space;
using WorkManagerSystemBackend.Core.Dtos.User;

namespace WorkManagerSystemBackend.Core.Dtos.UserSpace
{
    public class GetUserSpaceDto
    {
        public long Id { get; set; }
        public long UsersId { get; set; }
        public long SpaceId { get; set; }
        public SpaceGetDto Space { get; set; }
        public UserGetDto User { get; set; }
    }
}
