using System.ComponentModel.DataAnnotations.Schema;

namespace Timeboxed.Domain.Models
{
    public class EmployeePreferences
    {
        public Guid Id { get; set; }

        [ForeignKey("Employee")]
        public Guid EmployeeId { get; set; }

        public int? MinHours { get; set; }

        public int? MaxHours { get; set; }

        public virtual ICollection<EmployeeUnavailableDays> UnavailableDays { get; set; }
    }
}
