using System;

namespace Timeboxed.Api.Models.Requests
{
    public class UpdateUserPreferencesRequest
    {
        public Guid DefaultGroup { get; set; }
    }
}
