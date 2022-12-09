using System;
using System.Collections.Generic;
using Timeboxed.Api.Models.Responses.Common;

namespace Timeboxed.Api.Models.Responses
{
    public class RotaListResponse : TrackingResponse
    {
        public Guid? Id { get; set; }

        public Guid? GroupId { get; set; }

        public string Name { get; set; }

        public ICollection<Guid> Employees { get; set; } = new List<Guid>();

        public bool? Locked { get; set; }

        public string Colour { get; set; }
    }
}
