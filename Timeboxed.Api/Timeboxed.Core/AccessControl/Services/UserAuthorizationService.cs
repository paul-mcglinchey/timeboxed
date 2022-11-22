using Timeboxed.Core.AccessControl.Interfaces;
using Timeboxed.Data.Enums;

namespace Timeboxed.Core.AccessControl.Services
{
    public class UserAuthorizationService : IUserAuthorisationService<TimeboxedPermission>
    {
        public async Task<bool> IsAuthorised(List<TimeboxedPermission> requiredPermissions)
        {
            return true;
        }
    }
}
