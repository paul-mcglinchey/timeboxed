using System;
using Timeboxed.Domain.Models.Common;

namespace Timeboxed.Api.Models.Responses.Common
{
    public class PhoneNumberResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Number { get; set; }

        public static implicit operator PhoneNumberResponse(PhoneNumber phoneNumber) => new PhoneNumberResponse 
        { 
            Id = phoneNumber.Id, 
            Name = phoneNumber.Name,
            Description = phoneNumber.Description,
            Number = phoneNumber.Number
        };
    }
}
