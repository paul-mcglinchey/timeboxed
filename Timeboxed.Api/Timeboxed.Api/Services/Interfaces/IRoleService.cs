using System;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models;
using Timeboxed.Api.Models.DTOs;
using Timeboxed.Domain.Models;

namespace Timeboxed.Api.Services.Interfaces
{
    public interface IRoleService
    {
        public Task<ListResponse<RoleDto>> GetRolesAsync(CancellationToken cancellationToken);

        public Task<ListResponse<RoleDto>> GetRolesAsync(Guid groupId, CancellationToken cancellationToken);

        public Task<bool> RoleNameExistsAsync(string roleName, CancellationToken cancellationToken);

        public Task<Guid> CreateRoleAsync(Role role, CancellationToken cancellationToken);

        public Task<Guid> DeleteRoleAsync(Guid roleId, CancellationToken cancellationToken);
    }
}
