using System;
using System.Collections.Generic;

namespace Timeboxed.Api.Models.Requests
{
    public class UpdateSessionRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public List<UpdateTagRequest> Tags { get; set; } = new List<UpdateTagRequest>();

        public DateTime SessionDate { get; set; }
    }

    public class UpdateTagRequest
    {
        public Guid? GroupClientTagId { get; set; }

        public string Value { get; set; }
    }
}
