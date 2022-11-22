using System;
using System.Text.Json.Serialization;
using Timeboxed.Core.Converters;

namespace Timeboxed.Api.Models.Requests
{
    public class UpdateClientRequest
    {
        public string? FirstName { get; set; }

        public string? MiddleNames { get; set; } 

        public string? LastName { get; set; }

        public string? PrimaryEmailAddress { get; set; }

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

        public string Colour { get; set; }
    }
}
