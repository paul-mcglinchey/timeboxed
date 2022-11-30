using System;
using System.Collections.Generic;

namespace Timeboxed.Api.Models.Requests
{
    public class AddSessionRequest
    {
        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime SessionDate { get; set; }

        public ICollection<AddTagRequest> Tags { get; set; } = new List<AddTagRequest>();
    }

    public class AddTagRequest
    {
        public Guid? GroupClientTagId { get; set; }

        public string Value { get; set; }
    }
}
