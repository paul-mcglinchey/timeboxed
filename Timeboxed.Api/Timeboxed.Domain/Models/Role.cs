namespace Timeboxed.Domain.Models
{
    public class Role
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid? GroupId { get; set; }

        public Group? Group { get; set; }

        public int? ApplicationId { get; set; }

        public Application? Application { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }

        public virtual ICollection<GroupUser> GroupUsers { get; set; }
    }
}
