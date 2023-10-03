using System;

namespace  Timeboxed.Api.Models.Requests;

public class AdminGetUsersRequest
{
    public Guid? GroupId { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }
}