using Timeboxed.Domain.Models.Common;

namespace Timeboxed.Domain.Models
{
    public class Rota : Tracking
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public int? ClosingHour { get; set; }

        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();

        public bool? Locked { get; set; } = false;

        public string? Colour { get; set; }

        public Guid GroupId { get; set; }

        public Group Group { get; set; }
    }
}
