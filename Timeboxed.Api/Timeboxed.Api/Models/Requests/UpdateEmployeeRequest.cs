using System;
using System.Text.Json.Serialization;
using Timeboxed.Core.Converters;

namespace Timeboxed.Api.Models.Requests
{
    public class UpdateEmployeeRequest
    {
        public string FirstName { get; set; }

        public string? MiddleNames { get; set; }

        public string LastName { get; set; }

        public string PrimaryEmailAddress { get; set; }

        public string? PrimaryPhoneNumber { get; set; }

        public string? FirstLine { get; set; }

        public string? SecondLine { get; set; }

        public string? ThirdLine { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

        public string? PostCode { get; set; }

        public string? ZipCode { get; set; }

        [JsonConverter(typeof(NullableDateOnlyJsonConverter))]
        public DateOnly? BirthDate { get; set; }

        public Guid? Role { get; set; }

        public Guid? ReportsTo { get; set; }

        [JsonConverter(typeof(NullableDateOnlyJsonConverter))]
        public DateOnly? StartDate { get; set; }

        public int? MinHours { get; set; }

        public int? MaxHours { get; set; }

        public string Colour { get; set; }
    }
}
