using System;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Models.Responses.Common;
using Timeboxed.Domain.Models;

namespace Timeboxed.Api.Services.Interfaces
{
    public interface IGroupUserService
    {
        public Task<GroupUser?> GetGroupUserAsync(Guid groupId, Guid userId, CancellationToken cancellationToken);

        public Task<ListResponse<UserListResponse>> GetGroupUsersAsync(CancellationToken cancellationToken);

        public Task<GroupUserResponse> UpdateGroupUserAsync(Guid userId, UpdateGroupUserRequest request, CancellationToken cancellationToken);

        public Task<ListResponse<UserListResponse>> InviteGroupUserAsync(string usernameOrEmail, CancellationToken cancellationToken);

        public Task<Guid> DeleteUserAsync(Guid userId, CancellationToken cancellationToken);

        public Task<Guid?> JoinGroupAsync(CancellationToken cancellationToken);

        public Task<Guid?> LeaveGroupAsync(CancellationToken cancellationToken);
    }
}
