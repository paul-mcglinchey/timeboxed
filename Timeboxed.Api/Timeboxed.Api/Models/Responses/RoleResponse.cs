using System;
using System.Collections.Generic;
using System.Linq;
using Timeboxed.Domain.Models;

namespace Timeboxed.Api.Models.Responses
{
    public class RoleResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? ApplicationId { get; set; }

        public ICollection<int> Permissions { get; set; } = new List<int>();

        public static implicit operator RoleResponse(Role role) => new RoleResponse 
        { 
            Id = role.Id,
            Name = role.Name,
            Description = role.Description,
            ApplicationId = role.ApplicationId,
            Permissions = role.Permissions.Select(p => p.Id).ToList() 
        };
    }
}
