namespace WorkManagerSystemBackend.Core.Dtos.User
{
    public class UpdateUserPasswordDto
    {
        public long Id { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
