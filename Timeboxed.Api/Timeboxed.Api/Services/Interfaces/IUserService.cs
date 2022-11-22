using System;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models;
using Timeboxed.Api.Models.DTOs;

namespace Timeboxed.Api.Services.Interfaces
{
    public interface IUserService
    {
        public Task<bool> UserExistsAsync(UserRequest user, CancellationToken cancellationToken);

        public Task<bool> UserExistsAsync(Guid userId, CancellationToken cancellationToken);

        public Task<UserDto> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);

        public Task<UserDto> CreateUserAsync(UserRequest user, CancellationToken cancellationToken);

        public Task<UserDto> AuthenticateUserAsync(UserRequest user, CancellationToken cancellationToken);

        public Task<UserPreferencesDto> UpdateUserPreferencesAsync(UserPreferencesDto requestBody, CancellationToken cancellationToken);
    }
}
