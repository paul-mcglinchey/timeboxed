using System;

namespace Timeboxed.Api.Models.Requests;

public class GetSessionsRequest
{
    public Guid? TagId { get; set; }
}
