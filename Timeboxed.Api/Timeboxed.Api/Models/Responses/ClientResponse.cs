using System;
using System.Collections.Generic;
using Timeboxed.Core.Converters;
using Timeboxed.Api.Models.Responses.Common;
using Newtonsoft.Json;

namespace Timeboxed.Api.Models.Responses
{
    public class ClientResponse : TrackingResponse
    {
        public Guid Id { get; set; }

        public Guid? GroupId { get; set; }

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

        public List<Guid> Sessions { get; set; }

        public string Colour { get; set; }
    }
}
