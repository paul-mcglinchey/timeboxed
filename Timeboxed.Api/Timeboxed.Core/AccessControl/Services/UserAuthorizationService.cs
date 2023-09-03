using Timeboxed.Core.AccessControl.Interfaces;

namespace Timeboxed.Core.AccessControl.Services
{
    public class UserAuthorizationService : IUserAuthorisationService<int>
    {
        private readonly IUserContextProvider userContextProvider;

        public UserAuthorizationService(IUserContextProvider userContextProvider)
        {
            this.userContextProvider = userContextProvider;
        }

        public async Task<bool> IsAuthorised(List<int> requiredPermissions, bool adminRequired = false)
        {
            if (adminRequired && !this.userContextProvider.User.IsAdmin)
            {
                return false;
            }

            return true;
        }
    }
}
