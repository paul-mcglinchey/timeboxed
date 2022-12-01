namespace Timeboxed.Domain.Models
{
    public class UserAccessControl
    {
        public Guid Id { get; set; }

        public string UserEmail { get; set; }

        public string? AlphaAccessKey { get; set; }
    }
}
