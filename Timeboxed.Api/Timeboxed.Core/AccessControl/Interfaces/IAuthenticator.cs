using Timeboxed.Domain.Models;

namespace Timeboxed.Core.AccessControl.Interfaces
{
    public interface IAuthenticator
    {
        public Guid UserId { get; }

        public User User { get; }

        Task<bool> AuthenticateAsync();
    }
}
