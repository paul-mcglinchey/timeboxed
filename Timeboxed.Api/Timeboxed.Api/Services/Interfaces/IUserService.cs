using System;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;

namespace Timeboxed.Api.Services.Interfaces
{
    public interface IUserService
    {
        public Task<bool> UserExistsAsync(UserRequest user, CancellationToken cancellationToken);

        public Task<bool> UserExistsAsync(Guid userId, CancellationToken cancellationToken);

        public Task<UserResponse> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);

        public Task<UserResponse> CreateUserAsync(UserRequest user, CancellationToken cancellationToken);

        public Task<UserResponse> AuthenticateUserAsync(UserRequest user, CancellationToken cancellationToken);

        public Task<UserPreferencesResponse> UpdateUserPreferencesAsync(UserPreferencesResponse requestBody, CancellationToken cancellationToken);
    }
}
