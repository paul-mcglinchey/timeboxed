using System;
using System.Collections.Generic;

namespace Timeboxed.Api.Models.Responses
{
    public class GroupUserResponse
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public Guid GroupId { get; set; }

        public bool HasJoined { get; set; }

        public List<Guid> Roles { get; set; }

        public List<int> Applications { get; set; }
    }
}
