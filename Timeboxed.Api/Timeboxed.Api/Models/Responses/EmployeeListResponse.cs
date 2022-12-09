using System;
using System.Collections.Generic;
using Timeboxed.Api.Models.Responses.Common;

namespace Timeboxed.Api.Models.Responses
{
    public class EmployeeListResponse : TrackingResponse
    {
        public Guid? Id { get; set; }

        public Guid? GroupId { get; set; }

        public Guid? Role { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleNames { get; set; }

        public string PrimaryEmailAddress { get; set; }

        public List<EmployeeHolidayResponse> Holidays { get; set; }

        public int? MinHours { get; set; }

        public int? MaxHours { get; set; }

        public List<DayOfWeek> UnavailableDays { get; set; }

        public string? Colour { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
