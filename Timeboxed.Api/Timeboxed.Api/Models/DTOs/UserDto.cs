using System;

namespace Timeboxed.Api.Models.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool? IsAdmin { get; set; }

        public string Token { get; set; }

        public UserPreferencesDto? Preferences { get; set; }
    }
}
