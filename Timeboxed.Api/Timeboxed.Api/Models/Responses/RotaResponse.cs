using System;
using System.Collections.Generic;
using System.Linq;
using Timeboxed.Api.Models.Responses.Common;
using Timeboxed.Domain.Models;

namespace Timeboxed.Api.Models.Responses
{
    public class RotaResponse : TrackingResponse
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? ClosingHour { get; set; }

        public ICollection<Guid> Schedules { get; set; } = new List<Guid>();

        public ICollection<Guid> Employees { get; set; } = new List<Guid>();

        public bool? Locked { get; set; }

        public string Colour { get; set; }

        public Guid? GroupId { get; set; }

        public static implicit operator RotaResponse(Rota rota) => new RotaResponse
        {
            Id = rota.Id,
            Name = rota.Name,
            Description = rota.Description,
            ClosingHour = rota.ClosingHour,
            Locked = rota.Locked,
            Colour = rota.Colour,
            GroupId = rota.GroupId,
            Schedules = rota.Schedules.Select(s => s.Id).ToList(),
            Employees = rota.Employees.Select(e => e.EmployeeId).ToList(),
        };
    }
}
