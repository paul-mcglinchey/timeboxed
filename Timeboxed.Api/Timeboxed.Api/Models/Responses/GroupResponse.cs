using System;
using System.Collections.Generic;

namespace Timeboxed.Api.Models.Responses
{
    public class GroupListResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Colour { get; set; }

        public List<int> Applications { get; set; }

        public List<Guid> Users { get; set; }
    }

    public class GroupResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Colour { get; set; }

        public List<int> Applications { get; set; }

        public List<GroupUserResponse> GroupUsers { get; set; }
    }
}
