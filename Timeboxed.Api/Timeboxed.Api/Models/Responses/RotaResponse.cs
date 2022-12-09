using System;
using System.Collections.Generic;
using Timeboxed.Api.Models.Responses.Common;

namespace Timeboxed.Api.Models.Responses
{
    public class RotaResponse : TrackingResponse
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? ClosingHour { get; set; }

        public ICollection<ScheduleResponse> Schedules { get; set; } = new List<ScheduleResponse>();

        public ICollection<EmployeeListResponse> Employees { get; set; } = new List<EmployeeListResponse>();

        public bool? Locked { get; set; }

        public string Colour { get; set; }

        public Guid? GroupId { get; set; }
    }
}
