namespace Timeboxed.Api.Models
{
    public class UserRequest
    {
        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? UsernameOrEmail { get; set; }

        public string? Password { get; set; }
    }
}
