using Timeboxed.Domain.Models.Common;

namespace Timeboxed.Domain.Models
{
    public class Employee : Tracking
    {
        public Employee() { }

        public Employee(string firstName, string lastName, string primaryEmailAddress, Guid groupId, Guid userId)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            PrimaryEmailAddress = primaryEmailAddress;
            GroupId = groupId;

            this.AddTracking(userId, true);
        }

        public Guid Id { get; set; }

        public Guid GroupId { get; set; }

        public Group Group { get; set; }

        public Guid? LinkedUserId { get; set; }

        public virtual User LinkedUser { get; set; }

        public Guid? ReportsToId { get; set; }

        public virtual Employee? ReportsTo { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? MiddleNames { get; set; }

        public string FullName => $"{this.FirstName} {this.LastName}";

        public string PrimaryEmailAddress { get; set; }

        public string? PrimaryPhoneNumber { get; set; }

        public ICollection<Email> Emails { get; set; } = new List<Email>();

        public ICollection<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();

        public string? FirstLine { get; set; }

        public string? SecondLine { get; set; }

        public string? ThirdLine { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

        public string? PostCode { get; set; }

        public string? ZipCode { get; set; }

        public DateOnly? BirthDate { get; set; }

        public Guid? Role { get; set; }

        public DateOnly? StartDate { get; set; }

        public virtual ICollection<EmployeeHoliday> Holidays { get; set; } = new List<EmployeeHoliday>();

        public int? MinHours { get; set; }

        public int? MaxHours { get; set; }

        public virtual ICollection<EmployeeUnavailableDays> UnavailableDays { get; set; } = new List<EmployeeUnavailableDays>();

        public string? Colour { get; set; }

        public bool? IsDeleted { get; set; } = false;

        public ICollection<Rota> Rotas { get; set; } = new List<Rota>();
    }
}
