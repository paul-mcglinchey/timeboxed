namespace Timeboxed.Domain.Models
{
    public class EmployeeScheduleShift
    {
        public Guid Id { get; set; }

        public DateOnly Date { get; set; }

        public string? StartHour { get; set; }

        public string? EndHour { get; set; }

        public string? Notes { get; set; }
    }
}
