using System;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;

namespace Timeboxed.Api.Services.Interfaces
{
    public interface IGroupService
    {
        public Task<ListResponse<GroupResponse>> GetGroupsAsync(CancellationToken cancellationToken);

        public Task<GroupResponse> AddGroupAsync(AddGroupRequest request, CancellationToken cancellationToken);

        public Task<GroupResponse> UpdateGroupAsync(UpdateGroupRequest request, CancellationToken cancellationToken);

        public Task<Guid> DeleteGroupAsync(CancellationToken cancellationToken);

        public Task<bool> GroupExistsAsync(Guid groupId, CancellationToken cancellationToken);

        public Task<bool> GroupNameExistsAsync(string groupName, CancellationToken cancellationToken);

        public Task<bool> GroupNameExistsAsync(Guid groupId, string groupName, CancellationToken cancellationToken);
    }
}
