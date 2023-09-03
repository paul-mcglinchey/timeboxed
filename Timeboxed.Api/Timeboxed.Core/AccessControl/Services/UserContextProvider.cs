using Timeboxed.Core.AccessControl.Interfaces;
using Timeboxed.Domain.Models;

namespace Timeboxed.Core.AccessControl.Services
{
    public class UserContextProvider : IUserContextProvider
    {
        private readonly IAuthenticator authenticator;

        public UserContextProvider(IAuthenticator authenticator)
        {
            this.authenticator = authenticator;
        }

        public Guid UserId => this.authenticator.UserId;

        public User User => this.authenticator.User;
    }
}
