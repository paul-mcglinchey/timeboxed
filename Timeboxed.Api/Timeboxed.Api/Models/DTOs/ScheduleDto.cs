using Newtonsoft.Json;
using System;
using Timeboxed.Core.Converters;

namespace Timeboxed.Api.Models.DTOs
{
    public class ScheduleDto
    {
        public Guid? Id { get; set; }

        [JsonConverter(typeof(NullableDateOnlyJsonConverter))]
        public DateOnly StartDate { get; set; }

        public EmployeeScheduleDto[] EmployeeSchedules { get; set; }
    }
}
