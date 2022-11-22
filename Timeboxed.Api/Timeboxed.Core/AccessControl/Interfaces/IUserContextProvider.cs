namespace Timeboxed.Core.AccessControl.Interfaces
{
    using System;

    public interface IUserContextProvider
    {
        Guid UserId { get; }
    }
}
