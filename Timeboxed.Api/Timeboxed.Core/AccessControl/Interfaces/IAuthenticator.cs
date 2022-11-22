namespace Timeboxed.Core.AccessControl.Interfaces
{
    public interface IAuthenticator
    {
        public Guid UserId { get; }

        Task<bool> AuthenticateAsync();
    }
}
