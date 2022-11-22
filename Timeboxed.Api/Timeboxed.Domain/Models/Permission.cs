using System.ComponentModel.DataAnnotations.Schema;

namespace Timeboxed.Domain.Models
{
    public class Permission
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string Language { get; set; } = "en-US";

        [ForeignKey(nameof(Audit))]
        public Guid AuditId { get; set; }

        public ICollection<Application> Applications { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
