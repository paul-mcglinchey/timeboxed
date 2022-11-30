using System;

namespace Timeboxed.Api.Models.Responses
{
    public class UserListResponse
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool? IsAdmin { get; set; }
    }
}
