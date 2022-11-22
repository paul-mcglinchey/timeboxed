using System.ComponentModel.DataAnnotations.Schema;

namespace Timeboxed.Domain.Models
{
    public class Application
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string? Icon { get; set; }

        public string? BackgroundImage { get; set; }

        public string? BackgroundVideo { get; set; }

        public string Url { get; set; }

        public string? Colour { get; set; }

        [ForeignKey(nameof(Audit))]
        public Guid AuditId { get; set; }

        public ICollection<Permission> Permissions { get; set; }

        public ICollection<Group> Groups { get; set; }

        public ICollection<GroupUser> GroupUsers { get; set; }
    }
}
