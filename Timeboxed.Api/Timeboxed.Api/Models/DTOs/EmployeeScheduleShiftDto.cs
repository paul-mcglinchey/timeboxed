using Newtonsoft.Json;
using System;
using Timeboxed.Core.Converters;

namespace Timeboxed.Api.Models.DTOs
{
    public class EmployeeScheduleShiftDto
    {
        [JsonConverter(typeof(NullableDateOnlyJsonConverter))]
        public DateOnly? Date { get; set; }

        public string? StartHour { get; set; }

        public string? EndHour { get; set; }

        public string? Notes { get; set; }
    }
}
