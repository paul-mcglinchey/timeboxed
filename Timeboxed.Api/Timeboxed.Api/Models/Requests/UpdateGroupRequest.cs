using System;
using System.Collections.Generic;

namespace Timeboxed.Api.Models.Requests
{
    public class UpdateGroupRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Colour { get; set; }

        public List<UpdateGroupUserRequest> GroupUsers { get; set; }
    }

    public class UpdateGroupUserRequest
    {
        public List<Guid> Roles { get; set; }

        public List<int> Applications { get; set; }
    }
}
