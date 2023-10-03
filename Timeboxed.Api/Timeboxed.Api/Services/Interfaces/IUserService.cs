using System;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;

namespace Timeboxed.Api.Services.Interfaces
{
    public interface IUserService
    {
        public Task<bool> UserExistsAsync(string usernameOrEmail, CancellationToken cancellationToken);

        public Task<bool> UserExistsAsync(string email, string username, CancellationToken cancellationToken);

        public Task<bool> UserExistsAsync(Guid userId, CancellationToken cancellationToken);

        public Task<UserAuthResponse> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);

        public Task<UserAuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken);

        public Task<UserAuthResponse> SignupAsync(SignupRequest request, CancellationToken cancellationToken);

        public Task<UserPreferencesResponse> UpdateUserPreferencesAsync(UpdateUserPreferencesRequest request, CancellationToken cancellationToken);
    }
}
