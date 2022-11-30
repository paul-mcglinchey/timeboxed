using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Timeboxed.Core.Converters;
using Timeboxed.Domain.Models;

namespace Timeboxed.Api.Models.Responses
{
    public class ScheduleResponse
    {
        public Guid? Id { get; set; }

        [JsonConverter(typeof(NullableDateOnlyJsonConverter))]
        public DateOnly StartDate { get; set; }

        public ICollection<EmployeeScheduleResponse> EmployeeSchedules { get; set; } = new List<EmployeeScheduleResponse>();

        public static implicit operator ScheduleResponse(Schedule schedule) => new ScheduleResponse
        {
            Id = schedule.Id,
            StartDate = schedule.StartDate,
            EmployeeSchedules = schedule.EmployeeSchedules.Select<EmployeeSchedule, EmployeeScheduleResponse>(es => es).ToList(),
        };
    }

    public class EmployeeScheduleResponse
    {
        public Guid EmployeeId { get; set; }

        public ICollection<EmployeeScheduleShiftResponse> Shifts { get; set; } = new List<EmployeeScheduleShiftResponse>();

        public static implicit operator EmployeeScheduleResponse(EmployeeSchedule employeeSchedule) => new EmployeeScheduleResponse
        {
            EmployeeId = employeeSchedule.EmployeeId,
            Shifts = employeeSchedule.Shifts.Select<EmployeeScheduleShift, EmployeeScheduleShiftResponse>(s => s).ToList(),
        };
    }

    public class EmployeeScheduleShiftResponse
    {
        [JsonConverter(typeof(NullableDateOnlyJsonConverter))]
        public DateOnly? Date { get; set; }

        public string? StartHour { get; set; }

        public string? EndHour { get; set; }

        public string? Notes { get; set; }

        public static implicit operator EmployeeScheduleShiftResponse(EmployeeScheduleShift shift) => new EmployeeScheduleShift
        {
            Date = shift.Date,
            StartHour = shift.StartHour,
            EndHour = shift.EndHour,
            Notes = shift.Notes,
        };
    }
}
