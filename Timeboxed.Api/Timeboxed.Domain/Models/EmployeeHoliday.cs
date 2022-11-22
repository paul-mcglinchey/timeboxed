namespace Timeboxed.Domain.Models
{
    public class EmployeeHoliday
    {
        public Guid Id { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public bool? IsPaid { get; set; }

        public string? Notes { get; set; }
    }
}
