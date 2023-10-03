using System;

namespace Timeboxed.Api.Models.Responses
{
    public class UserAuthResponse
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool? IsAdmin { get; set; }

        public string Token { get; set; }

        public UserAuthPreferencesResponse Preferences { get; set; }
    }

    public class UserAuthPreferencesResponse
    {
        public Guid? Id { get; set; }

        public Guid? DefaultGroup { get; set; }
    }
}
