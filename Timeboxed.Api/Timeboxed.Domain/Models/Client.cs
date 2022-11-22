using System.ComponentModel.DataAnnotations;
using Timeboxed.Domain.Models.Common;

namespace Timeboxed.Domain.Models
{
    public class Client : Tracking
    {
        public Client() { }

        public Client(string firstName, string lastName, string primaryEmailAddress, string colour, Guid groupId, Guid userId)
        {
            this.Id = Guid.NewGuid();
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PrimaryEmailAddress = primaryEmailAddress;
            this.GroupId = groupId;
            this.Colour = colour;

            this.AddTracking(userId, true);
        }

        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string? MiddleNames { get; set; }

        public string FullName => $"{this.FirstName} {this.LastName}";

        [Required]
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

        public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

        public string? Colour { get; set; }

        public Guid GroupId { get; set; }
        
        public virtual Group Group { get; set; }
    }
}
