namespace Timeboxed.Api.Models.Requests
{
    public class LoginRequest
    {
        public string UsernameOrEmail { get; set; }

        public string Password { get; set; }
    }
}
