namespace Timeboxed.Domain.Models.Common
{
    public class PhoneNumber
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string Number { get; set; }
    }
}
