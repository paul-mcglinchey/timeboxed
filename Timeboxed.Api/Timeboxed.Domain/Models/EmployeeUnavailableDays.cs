using System.ComponentModel.DataAnnotations.Schema;

namespace Timeboxed.Domain.Models
{
    public class EmployeeUnavailableDays
    {
        public Guid Id { get; set; }

        [ForeignKey("EmployeePreferences")]
        public Guid EmployeePreferencesId { get; set; }

        public EmployeePreferences EmployeePreferences { get; set; }

        public DayOfWeek DayOfWeek { get; set; }
    }
}
