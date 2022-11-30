namespace Timeboxed.Api.Models.Requests
{
    public class UserRequest
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string UsernameOrEmail { get; set; }

        public string Password { get; set; }
    }
}
