using System.Collections.Generic;

namespace Timeboxed.Api.Models.Responses
{
    public class AddRoleRequest
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public int? ApplicationId { get; set; }

        public ICollection<int> Permissions { get; set; } = new List<int>();
    }
}
