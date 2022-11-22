namespace Timeboxed.Domain.Models
{
    public class Schedule
    {
        public Schedule()
        {
            Locked = StartDate.Day < new DateOnly().Day;
        }

        public Guid Id { get; set; }

        public DateOnly StartDate { get; set; }

        public ICollection<EmployeeSchedule> EmployeeSchedules { get; set; }

        public bool Locked { get; set; } = false;

        public Guid RotaId { get; set; }

        public Rota Rota { get; set; }
    }
}
