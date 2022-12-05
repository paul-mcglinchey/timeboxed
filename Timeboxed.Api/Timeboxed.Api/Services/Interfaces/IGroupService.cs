using System;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Models.Responses.Common;

namespace Timeboxed.Api.Services.Interfaces
{
    public interface IGroupService
    {
        public Task<ListResponse<GroupResponse>> GetGroupsAsync(CancellationToken cancellationToken);

        public Task<GroupResponse> GetGroupByIdAsync(Guid groupId, CancellationToken cancellationToken);

        public Task<Guid> AddGroupAsync(AddGroupRequest request, CancellationToken cancellationToken);

        public Task UpdateGroupAsync(UpdateGroupRequest request, CancellationToken cancellationToken);

        public Task DeleteGroupAsync(CancellationToken cancellationToken);

        public Task<bool> GroupExistsAsync(Guid groupId, CancellationToken cancellationToken);

        public Task<bool> GroupNameExistsAsync(string groupName, CancellationToken cancellationToken);

        public Task<bool> GroupNameExistsAsync(Guid groupId, string groupName, CancellationToken cancellationToken);
    }
}
