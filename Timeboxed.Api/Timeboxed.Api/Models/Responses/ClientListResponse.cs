using System;
using System.Collections.Generic;
using Timeboxed.Api.Models.Responses.Common;

namespace Timeboxed.Api.Models.Responses
{
    public class ClientListResponse : TrackingResponse
    {
        public Guid Id { get; set; }

        public Guid? GroupId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PrimaryEmailAddress { get; set; }

        public List<Guid> Sessions { get; set; }

        public string Colour { get; set; }
    }
}
