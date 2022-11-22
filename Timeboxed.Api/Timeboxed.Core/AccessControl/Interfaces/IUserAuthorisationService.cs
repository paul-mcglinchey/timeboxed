namespace Timeboxed.Core.AccessControl.Interfaces
{
    public interface IUserAuthorisationService<TPermission>
    {
        Task<bool> IsAuthorised(List<TPermission> requiredPermissions);
    }
}
