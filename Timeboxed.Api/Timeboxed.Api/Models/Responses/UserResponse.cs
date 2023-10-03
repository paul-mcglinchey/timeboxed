using System;
using System.Collections.Generic;

namespace Timeboxed.Api.Models.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool? IsAdmin { get; set; }

        public UserPreferencesResponse Preferences { get; set; }

        public List<UserResponseGroup> GroupRoles { get; set; } = new();
    }

    public class UserPreferencesResponse
    {
        public Guid? Id { get; set; }

        public Guid? DefaultGroup { get; set; }
    }

    public record UserResponseGroup
    {
        public Guid GroupId { get; set; }

        public IEnumerable<string> Roles { get; set; }

        public bool HasJoined { get; set; }
    }
}
