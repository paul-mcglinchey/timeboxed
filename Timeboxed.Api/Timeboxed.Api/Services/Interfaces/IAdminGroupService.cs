using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Models.Responses.Common;

namespace Timeboxed.Api.Services.Interfaces;

public interface IAdminGroupService
{
    Task<ListResponse<GroupResponse>> GetGroupsAsync(AdminGetGroupsRequest request, CancellationToken cancellationToken);

    Task UpdateGroupAsync(AdminUpdateGroupRequest request, CancellationToken cancellationToken);
}
