using Timeboxed.Domain.Models.Common;

namespace Timeboxed.Domain.Models
{
    public class Group : Tracking
    {
        public Group() { }

        public Group(string name, string description, string colour, List<Application> applications, List<GroupUser> groupUsers, Guid userId)
        {
            Name = name;
            Description = description;
            Colour = colour;
            Applications = applications;
            GroupUsers = groupUsers;

            this.AddTracking(userId, true);
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string? Colour { get; set; }

        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

        public virtual ICollection<GroupUser> GroupUsers { get; set; } = new List<GroupUser>();

        public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

        public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
    }
}
