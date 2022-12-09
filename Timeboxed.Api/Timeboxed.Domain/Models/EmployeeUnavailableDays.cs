namespace Timeboxed.Domain.Models
{
    public class EmployeeUnavailableDays
    {
        public Guid Id { get; set; }

        public Guid EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public byte DayOfWeek { get; set; }
    }
}
