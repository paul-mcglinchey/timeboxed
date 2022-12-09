using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Timeboxed.Core.Converters;

namespace Timeboxed.Api.Models.Responses
{
    public class ScheduleResponse
    {
        public Guid? Id { get; set; }

        [JsonConverter(typeof(NullableDateOnlyJsonConverter))]
        public DateOnly? StartDate { get; set; }

        public ICollection<EmployeeScheduleResponse> EmployeeSchedules { get; set; } = new List<EmployeeScheduleResponse>();
    }

    public class EmployeeScheduleResponse
    {
        public Guid EmployeeId { get; set; }

        public ICollection<EmployeeScheduleShiftResponse> Shifts { get; set; } = new List<EmployeeScheduleShiftResponse>();
    }

    public class EmployeeScheduleShiftResponse
    {
        [JsonConverter(typeof(NullableDateOnlyJsonConverter))]
        public DateOnly? Date { get; set; }

        public string? StartHour { get; set; }

        public string? EndHour { get; set; }

        public string? Notes { get; set; }
    }
}
