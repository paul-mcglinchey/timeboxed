using System;

namespace Timeboxed.Api.Models.Responses.Common
{
    public abstract class TrackingResponse
    {
        public DateTime? UpdatedAt { get; set; }

        public DateTime? CreatedAt { get; set; }

        public Guid? UpdatedBy { get; set; }

        public Guid? CreatedBy { get; set; }
    }
}
