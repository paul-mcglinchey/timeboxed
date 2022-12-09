using System;
using System.Linq.Expressions;
using Timeboxed.Api.Models.Responses.Common;
using Timeboxed.Domain.Models.Common;

namespace Timeboxed.Api.Services.Projections
{
    public class Common
    {
        public static Expression<Func<Email, EmailResponse>> MapEFEmailToResponse => (Email e) => new EmailResponse
        {
            Id = e.Id,
            Name = e.Name,
            Description = e.Description,
            Address = e.Address,
        };

        public static Expression<Func<PhoneNumber, PhoneNumberResponse>> MapEFPhoneNumberToResponse => (PhoneNumber ph) => new PhoneNumberResponse
        {
            Id = ph.Id,
            Name = ph.Name,
            Description = ph.Description,
            Number = ph.Number,
        };
    }
}
