using Timeboxed.Core.AccessControl.Interfaces;

namespace Timeboxed.Core.AccessControl.Services
{
    public class GroupContextProvider : IGroupContextProvider
    {
        private readonly IGroupValidator groupValidator;

        public GroupContextProvider(IGroupValidator groupValidator)
        {
            this.groupValidator = groupValidator;
        }

        public Guid GroupId => this.groupValidator.GroupId;
    }
}
