using System.Collections.Generic;

namespace Timeboxed.Api.Models.Responses
{
    public class PermissionResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Language { get; set; }

        public ICollection<int> Applications { get; set; } = new List<int>();
    }
}
