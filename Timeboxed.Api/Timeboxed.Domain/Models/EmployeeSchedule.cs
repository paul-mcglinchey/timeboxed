namespace Timeboxed.Domain.Models
{
    public class EmployeeSchedule
    {
        public Guid Id { get; set; }

        public Guid EmployeeId { get; set; }

        public virtual ICollection<EmployeeScheduleShift> Shifts { get; set; } = new List<EmployeeScheduleShift>();
    }
}
