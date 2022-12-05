using System;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Models.Responses.Common;

namespace Timeboxed.Api.Services.Interfaces
{
    public interface IGroupUserService
    {
        public Task<GroupUserResponse> GetGroupUserAsync(Guid groupId, Guid userId, CancellationToken cancellationToken);

        public Task<ListResponse<GroupUserResponse>> GetGroupUsersAsync(CancellationToken cancellationToken);

        public Task<GroupUserResponse> UpdateGroupUserAsync(Guid userId, UpdateGroupUserRequest request, CancellationToken cancellationToken);

        public Task InviteGroupUserAsync(string usernameOrEmail, CancellationToken cancellationToken);

        public Task UninviteGroupUserAsync(Guid userId, CancellationToken cancellationToken);

        public Task<Guid> DeleteUserAsync(Guid userId, CancellationToken cancellationToken);

        public Task<Guid?> JoinGroupAsync(CancellationToken cancellationToken);

        public Task<Guid?> LeaveGroupAsync(CancellationToken cancellationToken);
    }
}
