using System;

namespace Timeboxed.Api.Models.Responses.Common
{
    public class EmailResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }
    }
}
