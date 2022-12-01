namespace Timeboxed.Api.Models.Requests
{
    public class SignupRequest
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string AccessKey { get; set; }
    }
}
