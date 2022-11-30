using System;
using System.Collections.Generic;

namespace Timeboxed.Api.Models.Requests
{
    public class AddEditRotaRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int? ClosingHour { get; set; }

        public ICollection<Guid> Employees { get; set; } = new List<Guid>();

        public bool? Locked { get; set; }

        public string Colour { get; set; }
    }
}
