using Timeboxed.Domain.Models.Common;

namespace Timeboxed.Domain.Models
{
    public class Session : Tracking
    {
        public Session() { }

        public Session(string title, DateTime sessionDate, Guid clientId, Guid userId)
        {
            Id = Guid.NewGuid();
            Title = title;
            this.ClientId = clientId;
            SessionDate = sessionDate;

            this.AddTracking(userId, true);
        }

        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public Client Client { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

        public DateTime SessionDate { get; set; }
    }

    public class Tag
    {
        public Guid Id { get; set; }

        public Guid SessionId { get; set; }

        public Session Session { get; set; }

        public string Value { get; set; }
    }
}
