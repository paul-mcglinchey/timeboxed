using System;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Models.Responses.Common;

namespace Timeboxed.Api.Services.Interfaces
{
    public interface IRoleService
    {
        public Task<ListResponse<RoleResponse>> GetRolesAsync(Guid? groupId, CancellationToken cancellationToken);

        public Task<RoleResponse> GetRoleByIdAsync(Guid roleId, CancellationToken cancellationToken);

        public Task<bool> RoleNameExistsAsync(string roleName, CancellationToken cancellationToken);

        public Task<RoleResponse> CreateRoleAsync(AddRoleRequest request, CancellationToken cancellationToken);

        public Task<Guid> DeleteRoleAsync(Guid roleId, CancellationToken cancellationToken);
    }
}
