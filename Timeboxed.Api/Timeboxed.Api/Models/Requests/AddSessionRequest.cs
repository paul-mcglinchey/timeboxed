using System;
using System.Collections.Generic;

namespace Timeboxed.Api.Models.Requests
{
    public class AddUpdateSessionRequest
    {
        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime? SessionDate { get; set; }

        public ICollection<AddUpdateTagRequest> Tags { get; set; } = new List<AddUpdateTagRequest>();
    }

    public class AddUpdateTagRequest
    {
        public Guid Id { get; set; }

        public string Value { get; set; }
    }
}
