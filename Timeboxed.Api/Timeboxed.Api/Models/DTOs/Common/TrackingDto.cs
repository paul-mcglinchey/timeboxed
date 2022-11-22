using System;

namespace Timeboxed.Api.Models.DTOs.Common
{
    public class TrackingDto
    {
        public DateTime? UpdatedAt { get; set; }

        public DateTime? CreatedAt { get; set; }

        public Guid? UpdatedBy { get; set; }

        public Guid? CreatedBy { get; set; }
    }
}
