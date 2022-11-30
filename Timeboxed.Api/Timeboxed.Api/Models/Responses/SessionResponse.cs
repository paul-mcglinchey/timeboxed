using System;
using System.Collections.Generic;
using System.Linq;
using Timeboxed.Api.Models.Responses.Common;
using Timeboxed.Domain.Models;

namespace Timeboxed.Api.Models.Responses
{
    public class SessionResponse : TrackingResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<string> Tags { get; set; }

        public DateTime SessionDate { get; set; }

        public static implicit operator SessionResponse(Session session) => new SessionResponse
        {
            Id = session.Id,
            Title = session.Title,
            Description = session.Description,
            SessionDate = session.SessionDate,
            Tags = session.Tags.Select(t => t.GroupClientTag.Value).ToList(),
        };
    }
}
