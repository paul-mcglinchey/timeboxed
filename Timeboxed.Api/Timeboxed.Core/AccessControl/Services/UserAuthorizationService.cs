using Timeboxed.Core.AccessControl.Interfaces;

namespace Timeboxed.Core.AccessControl.Services
{
    public class UserAuthorizationService : IUserAuthorisationService<int>
    {
        public async Task<bool> IsAuthorised(List<int> requiredPermissions)
        {
            return true;
        }
    }
}
