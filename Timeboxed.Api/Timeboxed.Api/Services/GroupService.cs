using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<ListResponse<GroupListResponse>> GetGroupsAsync(CancellationToken cancellationToken)
        {
            var groups = this.context.Groups
                .Where(g => g.GroupUsers.Any(gu => gu.UserId.Equals(this.userContextProvider.UserId)))
                .Where(g => g.GroupUsers.Any(gu => gu.HasJoined))
                .Select(MapEFGroupToListResponse);

            return new ListResponse<GroupListResponse>
            {
                Items = await groups.ToListAsync(cancellationToken),
                Count = groups.Count(),
            };
        }

        public async Task<ListResponse<GroupListResponse>> GetGroupInvitesAsync(CancellationToken cancellationToken)
        {
            var groups = this.context.Groups
                .Where(g => g.GroupUsers.Any(gu => gu.UserId.Equals(this.userContextProvider.UserId)))
                .Where(g => g.GroupUsers.Any(gu => !gu.HasJoined))
                .Select(MapEFGroupToListResponse);

            return new ListResponse<GroupListResponse>
            {
                Items = await groups.ToListAsync(cancellationToken),
                Count = groups.Count(),
            };
        }

        public async Task<GroupResponse> GetGroupByIdAsync(Guid groupId, CancellationToken cancellationToken) =>
            await this.context.Groups
                .Where(g => g.Id == groupId)
                .Select(MapEFGroupToResponse)
                .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException($"Group {groupId} not found");

        public async Task<Guid> AddGroupAsync(AddGroupRequest request, CancellationToken cancellationToken)
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

            return group.Id;
        }

        public async Task UpdateGroupAsync(UpdateGroupRequest request, CancellationToken cancellationToken)
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
        }

        public async Task DeleteGroupAsync(CancellationToken cancellationToken)
        {
            var groupId = this.groupContextProvider.GroupId;

            var group = await this.context.Groups
                .Where(g => g.Id == groupId)
                .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException($"Group {groupId} not found");

            this.context.Groups.Remove(group);
            await this.context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> GroupExistsAsync(Guid groupId, CancellationToken cancellationToken) =>
            await this.context.Groups.Where(g => g.Id.Equals(groupId)).SingleOrDefaultAsync(cancellationToken) != null;

        public async Task<bool> GroupNameExistsAsync(string groupName, CancellationToken cancellationToken) =>
            await this.context.Groups.Where(g => g.Name.Equals(groupName)).SingleOrDefaultAsync(cancellationToken) != null;

        public async Task<bool> GroupNameExistsAsync(Guid groupId, string groupName, CancellationToken cancellationToken) =>
            await this.context.Groups.Where(g => !g.Id.Equals(groupId) && g.Name.Equals(groupName)).SingleOrDefaultAsync(cancellationToken) != null;

        private static Expression<Func<Group, GroupListResponse>> MapEFGroupToListResponse => (Group g) => new GroupListResponse
        {
            Id = g.Id,
            Name = g.Name,
            Description = g.Description,
            Colour = g.Colour,
            Applications = g.Applications.Select(a => a.Id).ToList(),
            Users = g.GroupUsers.Select(gu => gu.UserId).ToList(),
        };

        private static Expression<Func<Group, GroupResponse>> MapEFGroupToResponse => (Group g) => new GroupResponse
        {
            Id = g.Id,
            Name = g.Name,
            Description = g.Description,
            Colour = g.Colour,
            Applications = g.Applications.Select(a => a.Id).ToList(),
            Users = g.GroupUsers.Select(gu => new GroupUserResponse
            {
                Id = gu.Id,
                UserId = gu.UserId,
                Username = gu.User.Username,
                Email = gu.User.Email,
                GroupId = gu.GroupId,
                HasJoined = gu.HasJoined,
                Roles = gu.Roles.Select(r => r.Id).ToList(),
                Applications = gu.Applications.Select(gua => gua.Id).ToList()
            }).ToList(),
        };
    }
}
