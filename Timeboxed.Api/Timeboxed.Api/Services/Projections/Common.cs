using System;
using System.Linq;
using System.Linq.Expressions;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Models.Responses.Common;
using Timeboxed.Domain.Models;
using Timeboxed.Domain.Models.Common;

namespace Timeboxed.Api.Services.Projections
{
    public class Common
    {
        public static Expression<Func<Group, GroupResponse>> MapEFGroupToResponse => (Group g) => new GroupResponse
        {
            Id = g.Id,
            Name = g.Name,
            Description = g.Description,
            Colour = g.Colour,
            Applications = g.Applications.Select(a => a.Id).ToList(),
            Users = g.GroupUsers.Select(gu => new GroupUserResponse
            {
                Id = gu.Id,
                UserId = gu.UserId,
                Username = gu.User.Username,
                Email = gu.User.Email,
                GroupId = gu.GroupId,
                HasJoined = gu.HasJoined,
                Roles = gu.Roles.Select(r => r.Id).ToList(),
                Applications = gu.Applications.Select(gua => gua.Id).ToList()
            }).ToList(),
        };

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
