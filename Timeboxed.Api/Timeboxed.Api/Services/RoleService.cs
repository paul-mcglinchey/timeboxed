using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Models.Responses.Common;
using Timeboxed.Api.Services.Interfaces;
using Timeboxed.Core.AccessControl.Interfaces;
using Timeboxed.Core.Exceptions;
using Timeboxed.Data;
using Timeboxed.Domain.Models;

namespace Timeboxed.Api.Services
{
    public class RoleService : IRoleService
    {
        private IUserContextProvider userContextProvider;
        private IGroupContextProvider groupContextProvider;
        private TimeboxedContext context;

        public RoleService(IUserContextProvider userContextProvider, TimeboxedContext context, IGroupContextProvider groupContextProvider)
        {
            this.userContextProvider = userContextProvider;
            this.context = context;
            this.groupContextProvider = groupContextProvider;
        }

        public async Task<ListResponse<RoleResponse>> GetRolesAsync(Guid? groupId, CancellationToken cancellationToken)
        {
            var roles = await this.context.Roles
                .Where(r => groupId != null ? r.Group == null : (r.Group == null || r.Group.Id.Equals(groupId)))
                .Include(r => r.Permissions)
                .Include(r => r.Application)
                .Select<Role, RoleResponse>(r => r)
                .ToListAsync(cancellationToken);

            return new ListResponse<RoleResponse>
            {
                Items = roles,
                Count = roles.Count
            };
        }

        public async Task<RoleResponse> GetRoleByIdAsync(Guid roleId, CancellationToken cancellationToken) =>
            await this.context.Roles
                .Where(r => r.Id == roleId)
                .Select<Role, RoleResponse>(r => r)
                .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException($"Role {roleId} not found");

        public async Task<RoleResponse> CreateRoleAsync(AddRoleRequest request, CancellationToken cancellationToken)
        {
            var permissions = await this.context.Permissions.Where(p => request.Permissions.Contains(p.Id)).ToListAsync(cancellationToken);

            var role = new Role
            {
                Name = request.Name,
                Description = request.Description,
                ApplicationId = request.ApplicationId,
                Permissions = permissions,
                GroupId = this.groupContextProvider.GroupId,
            };

            this.context.Roles.Add(role);
            await this.context.SaveChangesAsync(cancellationToken);

            return role;
        }

        public async Task<Guid> DeleteRoleAsync(Guid roleId, CancellationToken cancellationToken)
        {
            var role = await this.context.Roles.Where(r => r.Id.Equals(roleId)).SingleOrDefaultAsync(cancellationToken);
            this.context.Roles.Remove(role);
            await this.context.SaveChangesAsync(cancellationToken);

            return role.Id;
        }

        public async Task<bool> RoleNameExistsAsync(string roleName, CancellationToken cancellationToken)
        {
            return await this.context.Roles.Where(r => r.GroupId.Equals(this.groupContextProvider.GroupId) && r.Name.Equals(roleName)).SingleOrDefaultAsync(cancellationToken) != null;
        }
    }
}
