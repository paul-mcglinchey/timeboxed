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

        public List<TagResponse> Tags { get; set; }

        public DateTime SessionDate { get; set; }

        public static implicit operator SessionResponse(Session session) => new SessionResponse 
        { 
            Id = session.Id,
            Title = session.Title,
            Description = session.Description,
            SessionDate = session.SessionDate,
            Tags = session.Tags.Select<Tag, TagResponse>(t => t).ToList(),
            UpdatedAt = session.UpdatedAt,
            UpdatedBy = session.UpdatedBy,
            CreatedAt = session.CreatedAt,
            CreatedBy = session.CreatedBy,
        };
    }

    public class TagResponse
    {
        public Guid Id { get; set; }

        public string Value { get; set; }

        public static implicit operator TagResponse(Tag tag) => new TagResponse { Id = tag.Id, Value = tag.Value };
    }
}
