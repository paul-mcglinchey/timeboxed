namespace Timeboxed.Domain.Models.Common
{
    public class Email
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string Address { get; set; }
    }
}
