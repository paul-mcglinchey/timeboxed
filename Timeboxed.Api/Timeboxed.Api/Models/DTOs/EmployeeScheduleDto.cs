using System;

namespace Timeboxed.Api.Models.DTOs
{
    public class EmployeeScheduleDto
    {
        public Guid EmployeeId { get; set; }

        public EmployeeScheduleShiftDto[] Shifts { get; set; }
    }
}
