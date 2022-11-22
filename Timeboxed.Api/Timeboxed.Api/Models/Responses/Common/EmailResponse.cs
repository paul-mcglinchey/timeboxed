using System;
using Timeboxed.Domain.Models.Common;

namespace Timeboxed.Api.Models.Responses.Common
{
    public class EmailResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public static implicit operator EmailResponse(Email email) => new EmailResponse 
        { 
            Id = email.Id, 
            Name = email.Name, 
            Description = email.Description, 
            Address = email.Address 
        };
    }
}
