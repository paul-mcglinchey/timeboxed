namespace Timeboxed.Core.AccessControl.Interfaces
{
    public interface IUserValidator
    {
        public bool TryValidate(string userId, out Guid userIdGuid);
    }
}
