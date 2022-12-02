using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Models.Responses.Common;
using Timeboxed.Api.Services.Interfaces;
using Timeboxed.Core.AccessControl.Interfaces;
using Timeboxed.Core.Exceptions;
using Timeboxed.Data;
using Timeboxed.Domain.Models;

namespace Timeboxed.Api.Services
{
    public class GroupService : IGroupService
    {
        private readonly IUserContextProvider userContextProvider;
        private readonly IGroupContextProvider groupContextProvider;
        private readonly TimeboxedContext context;

        public GroupService(IUserContextProvider userContextProvider, IGroupContextProvider groupContextProvider, TimeboxedContext context)
        {
            this.userContextProvider = userContextProvider;
            this.groupContextProvider = groupContextProvider;
            this.context = context;
        }

        public async Task<ListResponse<GroupResponse>> GetGroupsAsync(CancellationToken cancellationToken)
        {
            var groups = this.context.Groups
                .Where(g => g.GroupUsers.Any(gu => gu.UserId.Equals(this.userContextProvider.UserId)))
                .Include(g => g.Applications)
                .Include(g => g.GroupUsers)
                .ThenInclude(gu => gu.Applications)
                .Include(g => g.GroupUsers)
                .ThenInclude(gu => gu.Roles)
                .Select<Group, GroupResponse>(g => g);

            return new ListResponse<GroupResponse>
            {
                Items = await groups.ToListAsync(cancellationToken),
                Count = groups.Count(),
            };
        }

        public async Task<GroupResponse> GetGroupByIdAsync(Guid groupId, CancellationToken cancellationToken) =>
            await this.context.Groups
                .Where(g => g.Id == groupId)
                .Select<Group, GroupResponse>(g => g)
                .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException($"Group {groupId} not found");

        public async Task<GroupResponse> AddGroupAsync(AddGroupRequest request, CancellationToken cancellationToken)
        {
            var roles = await this.context.Roles
                .Where(r => r.ApplicationId == null || request.Applications.Contains(r.Application.Id))
                .ToListAsync(cancellationToken);

            var applications = await this.context.Applications
                .Where(a => request.Applications.Contains(a.Id))
                .ToListAsync();

            var groupUser = new GroupUser
            {
                UserId = this.userContextProvider.UserId,
                Roles = roles,
                HasJoined = true,
                Applications = applications,
            };

            var group = new Group(
                request.Name,
                request.Description,
                request.Colour,
                applications,
                new List<GroupUser> { groupUser },
                this.userContextProvider.UserId);

            this.context.Groups.Add(group);
            await this.context.SaveChangesAsync(cancellationToken);

            return group;
        }

        public async Task<GroupResponse> UpdateGroupAsync(UpdateGroupRequest request, CancellationToken cancellationToken)
        {
            var groupId = this.groupContextProvider.GroupId;

            var group = await this.context.Groups
                .Where(g => g.Id == groupId)
                .Include(g => g.Applications)
                .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException($"Group {groupId} not found");

            group.Name = request.Name;
            group.Description = request.Description;
            group.Colour = request.Colour;
            group.Applications = await this.context.Applications.Where(a => request.Applications.Contains(a.Id)).ToListAsync(cancellationToken);

            group.AddTracking(this.userContextProvider.UserId);

            await this.context.SaveChangesAsync(cancellationToken);

            return await this.GetGroupByIdAsync(groupId, cancellationToken);
        }

        public async Task<Guid> DeleteGroupAsync(CancellationToken cancellationToken)
        {
            var groupId = this.groupContextProvider.GroupId;

            var group = await this.context.Groups
                .Where(g => g.Id == groupId)
                .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException($"Group {groupId} not found");

            this.context.Groups.Remove(group);
            await this.context.SaveChangesAsync(cancellationToken);

            return group.Id;
        }

        public async Task<bool> GroupExistsAsync(Guid groupId, CancellationToken cancellationToken) =>
            await this.context.Groups.Where(g => g.Id.Equals(groupId)).SingleOrDefaultAsync(cancellationToken) != null;

        public async Task<bool> GroupNameExistsAsync(string groupName, CancellationToken cancellationToken) =>
            await this.context.Groups.Where(g => g.Name.Equals(groupName)).SingleOrDefaultAsync(cancellationToken) != null;

        public async Task<bool> GroupNameExistsAsync(Guid groupId, string groupName, CancellationToken cancellationToken) =>
            await this.context.Groups.Where(g => !g.Id.Equals(groupId) && g.Name.Equals(groupName)).SingleOrDefaultAsync(cancellationToken) != null;
    }
}
