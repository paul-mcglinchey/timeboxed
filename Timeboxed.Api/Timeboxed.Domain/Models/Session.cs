using Timeboxed.Domain.Models.Common;

namespace Timeboxed.Domain.Models
{
    public class Session : Tracking
    {
        public Session() { }

        public Session(string title, DateTime sessionDate, Guid clientId, Guid userId)
        {
            this.Id = Guid.NewGuid();
            this.Title = title;
            this.ClientId = clientId;
            this.SessionDate = sessionDate;

            this.AddTracking(userId, true);
        }

        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public Client Client { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public ICollection<SessionTag> Tags { get; set; } = new List<SessionTag>();

        public DateTime SessionDate { get; set; }
    }

    public class SessionTag
    {
        public Guid Id { get; set; }

        public Guid SessionId { get; set; }

        public Session Session { get; set; }

        public Guid GroupClientTagId { get; set; }

        public GroupClientTag GroupClientTag { get; set; }
    }

    public class GroupClientTag
    {
        public Guid Id { get; set; }

        public Guid GroupId { get; set; }

        public Group Group { get; set; }

        public ICollection<SessionTag> Tags { get; set; } = new List<SessionTag>();

        public string Value { get; set; }
    }
}
