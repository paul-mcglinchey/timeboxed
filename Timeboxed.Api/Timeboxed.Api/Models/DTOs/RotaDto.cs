using System;
using Timeboxed.Api.Models.DTOs.Common;

namespace Timeboxed.Api.Models.DTOs
{
    public class RotaDto : TrackingDto
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public int? ClosingHour { get; set; }

        public Guid[] Schedules { get; set; }

        public Guid[] Employees { get; set; }

        public bool? Locked { get; set; }

        public string? Colour { get; set; }

        public Guid? GroupId { get; set; }
    }
}
