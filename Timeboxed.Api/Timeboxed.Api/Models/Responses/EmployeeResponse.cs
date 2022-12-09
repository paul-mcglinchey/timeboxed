using System;
using System.Collections.Generic;
using Timeboxed.Api.Models.Responses.Common;
using Timeboxed.Core.Converters;
using Newtonsoft.Json;

namespace Timeboxed.Api.Models.Responses
{
    public class EmployeeResponse : TrackingResponse
    {
        public Guid? Id { get; set; }

        public Guid? GroupId { get; set; }

        public Guid? Role { get; set; }

        public Guid? ReportsTo { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleNames { get; set; }

        public string PrimaryEmailAddress { get; set; }

        public string PrimaryPhoneNumber { get; set; }

        public List<EmailResponse> Emails { get; set; }

        public List<PhoneNumberResponse> PhoneNumbers { get; set; }

        public string? FirstLine { get; set; }

        public string? SecondLine { get; set; }

        public string? ThirdLine { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

        public string? PostCode { get; set; }

        public string? ZipCode { get; set; }

        [JsonConverter(typeof(NullableDateOnlyJsonConverter))]
        public DateOnly? BirthDate { get; set; }

        [JsonConverter(typeof(NullableDateOnlyJsonConverter))]
        public DateOnly? StartDate { get; set; }

        public List<EmployeeHolidayResponse> Holidays { get; set; }

        public int? MinHours { get; set; }

        public int? MaxHours { get; set; }

        public List<DayOfWeek> UnavailableDays { get; set; }

        public string? Colour { get; set; }

        public bool? IsDeleted { get; set; }
    }

    public class EmployeeHolidayResponse
    {
        public Guid? Id { get; set; }

        [JsonConverter(typeof(NullableDateOnlyJsonConverter))]
        public DateOnly? StartDate { get; set; }

        [JsonConverter(typeof(NullableDateOnlyJsonConverter))]
        public DateOnly? EndDate { get; set; }

        public bool? IsPaid { get; set; }

        public string? Notes { get; set; }
    }
}
