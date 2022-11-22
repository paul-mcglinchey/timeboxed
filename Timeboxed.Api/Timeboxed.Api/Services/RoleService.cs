using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models;
using Timeboxed.Api.Models.DTOs;
using Timeboxed.Api.Services.Interfaces;
using Timeboxed.Core.AccessControl.Interfaces;
using Timeboxed.Data;
using Timeboxed.Domain.Models;

namespace Timeboxed.Api.Services
{
    public class RoleService : IRoleService
    {
        private IUserContextProvider userContextProvider;
        private IGroupContextProvider groupContextProvider;
        private TimeboxedContext context;
        private IMapper mapper;

        public RoleService(IUserContextProvider userContextProvider, TimeboxedContext context, IMapper mapper, IGroupContextProvider groupContextProvider)
        {
            this.userContextProvider = userContextProvider;
            this.context = context;
            this.mapper = mapper;
            this.groupContextProvider = groupContextProvider;
        }

        public async Task<ListResponse<RoleDto>> GetRolesAsync(CancellationToken cancellationToken)
        {
            var roles = await this.context.Roles
                .Where(r => r.Group == null)
                .Include(r => r.Permissions)
                .Include(r => r.Application)
                .ToListAsync(cancellationToken);

            return new ListResponse<RoleDto>
            {
                Items = this.mapper.Map<List<RoleDto>>(roles),
                Count = roles.Count
            };
        }

        public async Task<ListResponse<RoleDto>> GetRolesAsync(Guid groupId, CancellationToken cancellationToken)
        {
            var roles = await this.context.Roles
                .Where(r => r.Group == null || r.Group.Id.Equals(groupId))
                .Include(r => r.Permissions)
                .Include(r => r.Application)
                .ToListAsync(cancellationToken);

            return new ListResponse<RoleDto>
            {
                Items = this.mapper.Map<List<RoleDto>>(roles),
                Count = roles.Count
            };
        }

        public async Task<Guid> CreateRoleAsync(Role role, CancellationToken cancellationToken)
        {
            role.GroupId = this.groupContextProvider.GroupId;
            this.context.Roles.Add(role);
            await this.context.SaveChangesAsync(cancellationToken);

            return role.Id;
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
