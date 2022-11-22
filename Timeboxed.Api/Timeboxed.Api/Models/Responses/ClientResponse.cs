using System;
using System.Collections.Generic;
using Timeboxed.Core.Converters;
using Timeboxed.Domain.Models;
using System.Linq;
using Timeboxed.Domain.Models.Common;
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

        public List<SessionResponse> Sessions { get; set; }

        public string Colour { get; set; }

        public static implicit operator ClientResponse(Client client) => new ClientResponse
        {
            Id = client.Id,
            GroupId = client.GroupId,
            FirstName = client.FirstName,
            LastName = client.LastName,
            MiddleNames = client.MiddleNames,
            PrimaryEmailAddress = client.PrimaryEmailAddress,
            PrimaryPhoneNumber = client.PrimaryPhoneNumber,
            Emails = client.Emails.Select<Email, EmailResponse>(e => e).ToList(),
            PhoneNumbers = client.PhoneNumbers.Select<PhoneNumber, PhoneNumberResponse>(pn => pn).ToList(),
            FirstLine = client.FirstLine,
            SecondLine = client.SecondLine,
            ThirdLine = client.ThirdLine,
            City = client.City,
            Country = client.Country,
            PostCode = client.PostCode,
            ZipCode = client.ZipCode,
            BirthDate = client.BirthDate,
            Sessions = client.Sessions.Select<Session, SessionResponse>(s => s).ToList(),
            Colour = client.Colour,
            UpdatedAt = client.UpdatedAt,
            CreatedAt = client.CreatedAt,
            UpdatedBy = client.UpdatedBy,
            CreatedBy = client.CreatedBy,
        };
    }
}
