using System;
using System.Collections.Generic;
using System.Linq;
using Timeboxed.Domain.Models;

namespace Timeboxed.Api.Models.Responses
{
    public class GroupResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Colour { get; set; }

        public List<int> Applications { get; set; }

        public List<GroupUserResponse> GroupUsers { get; set; }

        public static implicit operator GroupResponse(Group g) => new GroupResponse
        {
            Id = g.Id,
            Name = g.Name,
            Description = g.Description,
            Colour = g.Colour,
            Applications = g.Applications.Select(a => a.Id).ToList(),
            GroupUsers = g.GroupUsers.Select<GroupUser, GroupUserResponse>(gu => gu).ToList(),
        };
    }

    public class GroupUserResponse
    {
        public Guid UserId { get; set; }

        public bool HasJoined { get; set; }

        public List<Guid> Roles { get; set; }

        public List<int> Applications { get; set; }

        public static implicit operator GroupUserResponse(GroupUser gu) => new GroupUserResponse
        {
            UserId = gu.UserId,
            HasJoined = gu.HasJoined,
            Roles = gu.Roles.Select(r => r.Id).ToList(),
            Applications = gu.Applications.Select(gua => gua.Id).ToList()
        };
    }
}
