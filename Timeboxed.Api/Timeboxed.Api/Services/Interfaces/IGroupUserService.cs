using System;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;

namespace Timeboxed.Api.Services.Interfaces
{
    public interface IGroupUserService
    {
        public Task<GroupUserResponse> GetGroupUserByIdAsync(Guid userId, CancellationToken cancellationToken);

        public Task UpdateGroupUserAsync(Guid userId, UpdateGroupUserRequest request, CancellationToken cancellationToken);

        public Task<Guid> InviteGroupUserAsync(string usernameOrEmail, CancellationToken cancellationToken);

        public Task UninviteGroupUserAsync(Guid userId, CancellationToken cancellationToken);

        public Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken);

        public Task JoinGroupAsync(CancellationToken cancellationToken);

        public Task LeaveGroupAsync(CancellationToken cancellationToken);
    }
}
