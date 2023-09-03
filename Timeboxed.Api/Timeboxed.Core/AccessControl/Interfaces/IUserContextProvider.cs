namespace Timeboxed.Core.AccessControl.Interfaces
{
    using System;
    using Timeboxed.Domain.Models;

    public interface IUserContextProvider
    {
        Guid UserId { get; }

        User User { get; }
    }
}
