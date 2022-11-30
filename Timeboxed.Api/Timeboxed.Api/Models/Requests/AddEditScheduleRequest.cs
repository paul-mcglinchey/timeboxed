using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Timeboxed.Core.Converters;

namespace Timeboxed.Api.Models.Requests
{
    public class AddEditScheduleRequest
    {
        [JsonConverter(typeof(NullableDateOnlyJsonConverter))]
        public DateOnly StartDate { get; set; }

        public ICollection<AddEditEmployeeScheduleRequest> EmployeeSchedules { get; set; } = new List<AddEditEmployeeScheduleRequest>();
    }

    public class AddEditEmployeeScheduleRequest
    {
        public Guid EmployeeId { get; set; }

        public ICollection<AddEditEmployeeScheduleShiftRequest> Shifts { get; set; } = new List<AddEditEmployeeScheduleShiftRequest>();
    }

    public class AddEditEmployeeScheduleShiftRequest
    {
        [JsonConverter(typeof(NullableDateOnlyJsonConverter))]
        public DateOnly Date { get; set; }

        public string? StartHour { get; set; }

        public string? EndHour { get; set; }

        public string? Notes { get; set; }
    }
}
