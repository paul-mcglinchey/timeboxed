using System;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models;
using Timeboxed.Api.Models.DTOs;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Domain.Models;

namespace Timeboxed.Api.Services.Interfaces
{
    public interface IGroupUserService
    {
        public Task<GroupUser?> GetGroupUserAsync(Guid groupId, Guid userId, CancellationToken cancellationToken);

        public Task<ListResponse<UserDto>> GetGroupUsersAsync(CancellationToken cancellationToken);

        public Task<GroupUserResponse> UpdateGroupUserAsync(Guid userId, UpdateGroupUserRequest request, CancellationToken cancellationToken);

        public Task<Guid> AddUserAsync(Guid userId, CancellationToken cancellationToken);

        public Task<Guid> DeleteUserAsync(Guid userId, CancellationToken cancellationToken);

        public Task<Guid?> JoinGroupAsync(CancellationToken cancellationToken);

        public Task<Guid?> LeaveGroupAsync(CancellationToken cancellationToken);
    }
}
