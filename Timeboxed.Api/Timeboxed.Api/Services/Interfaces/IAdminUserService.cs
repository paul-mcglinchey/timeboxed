using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Models.Responses.Common;

namespace Timeboxed.Api.Services.Interfaces;

public interface IAdminUserService
{
    Task<ListResponse<UserResponse>> GetUsersAsync(AdminGetUsersRequest request, CancellationToken cancellationToken);
}