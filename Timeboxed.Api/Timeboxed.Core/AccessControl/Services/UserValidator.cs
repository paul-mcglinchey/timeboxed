using Timeboxed.Core.AccessControl.Interfaces;
using Timeboxed.Data;

namespace Timeboxed.Core.AccessControl.Services
{
    public class UserValidator : IUserValidator
    {
        private readonly TimeboxedContext context;

        public UserValidator(TimeboxedContext context)
        {
            this.context = context;
        }

        public bool TryValidate(string userId, out Guid userIdGuid)
        {
            if (!Guid.TryParse(userId, out userIdGuid))
            {
                return false;
            }

            if (!UserExists(userIdGuid))
            {
                return false;
            }

            return true;
        }

        public bool UserExists(Guid userIdGuid)
        {
            return this.context.Users.Where(u => u.Id.Equals(userIdGuid)).SingleOrDefault() != null;
        }
    }
}
