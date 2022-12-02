using Timeboxed.Core.AccessControl.Interfaces;
using Timeboxed.Data.Constants;

namespace Timeboxed.Core.AccessControl.Services
{
    public class UserAuthorizationService : IUserAuthorisationService<TimeboxedPermissions>
    {
        public async Task<bool> IsAuthorised(List<TimeboxedPermissions> requiredPermissions)
        {
            return true;
        }
    }
}
