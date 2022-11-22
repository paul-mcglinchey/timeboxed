using System;

namespace Timeboxed.Api.Models.Requests
{
    public class AddSessionRequest
    {
        public string Title { get; set; }

        public DateTime SessionDate { get; set; }
    }
}
