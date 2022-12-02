using Microsoft.EntityFrameworkCore;
using System;
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
using Timeboxed.Data.Constants;
using Timeboxed.Domain.Models;

namespace Timeboxed.Api.Services
{
    public class GroupUserService : IGroupUserService
    {
        private readonly IUserContextProvider userContextProvider;
        private readonly IGroupContextProvider groupContextProvider;
        private readonly TimeboxedContext context;

        public GroupUserService(IUserContextProvider userContextProvider, IGroupContextProvider groupContextProvider, TimeboxedContext context)
        {
            this.userContextProvider = userContextProvider;
            this.groupContextProvider = groupContextProvider;
            this.context = context;
        }

        public async Task<GroupUser?> GetGroupUserAsync(Guid groupId, Guid userId, CancellationToken cancellationToken)
        {
            return await this.context.GroupUsers
                .Where(gu => gu.GroupId.Equals(groupId) && gu.UserId.Equals(userId))
                .Include(gu => gu.Roles)
                .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<ListResponse<UserListResponse>> GetGroupUsersAsync(CancellationToken cancellationToken)
        {
            var users = await this.context.Users
                .Where(u => this.context.GroupUsers
                    .Where(gu => gu.GroupId.Equals(this.groupContextProvider.GroupId))
                    .Any(gu => gu.UserId.Equals(u.Id)))
                .Select(u => new UserListResponse
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    IsAdmin = u.IsAdmin,
                })
                .ToListAsync(cancellationToken);

            return new ListResponse<UserListResponse>
            {
                Items = users,
                Count = users.Count,
            };
        }

        public async Task<GroupUserResponse> UpdateGroupUserAsync(Guid userId, UpdateGroupUserRequest request, CancellationToken cancellationToken)
        {
            var groupUser = await this.context.GroupUsers
                .Where(gu => gu.UserId == userId && gu.GroupId == this.groupContextProvider.GroupId)
                .Include(gu => gu.Roles)
                .Include(gu => gu.Applications)
                .FirstOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException($"User {userId} in group {this.groupContextProvider.GroupId} not found");

            var applications = await this.context.Applications.Where(a => request.Applications.Contains(a.Id)).ToListAsync(cancellationToken);
            var roles = await this.context.Roles.Where(r => request.Roles.Contains(r.Id)).ToListAsync(cancellationToken);

            groupUser.Applications = applications;
            groupUser.Roles = roles;

            await this.context.SaveChangesAsync(cancellationToken);

            return groupUser;
        }

        public async Task<ListResponse<UserListResponse>> InviteGroupUserAsync(string usernameOrEmail, CancellationToken cancellationToken)
        {
            var groupId = this.groupContextProvider.GroupId;

            var user = await this.context.Users
                .Where(u => u.Email == usernameOrEmail || u.Username == usernameOrEmail)
                .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException($"User with identity {usernameOrEmail} not found");

            var group = await this.context.Groups
                .Where(g => g.Id == groupId)
                .Include(g => g.GroupUsers)
                .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException($"Group {groupId} not found");

            var groupUser = group.GroupUsers.Where(gu => gu.UserId == user.Id).SingleOrDefault();

            if (groupUser != null)
            {
                throw new BadRequestException(groupUser.HasJoined ? $"User {usernameOrEmail} is already a member of the group" : $"User {usernameOrEmail} has already been invited to the group");
            }

            group.GroupUsers.Add(new GroupUser
            {
                UserId = user.Id,
                HasJoined = false,
                Roles = await this.context.Roles.Where(r => r.Id == Guid.Parse(TimeboxedRoles.GroupMember)).ToListAsync(cancellationToken),
            });

            await this.context.SaveChangesAsync(cancellationToken);

            return await this.GetGroupUsersAsync(cancellationToken);
        }

        public async Task UninviteGroupUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            var groupId = this.groupContextProvider.GroupId;

            var group = await this.context.Groups
                .Where(g => g.Id == groupId)
                .Include(g => g.GroupUsers)
                .SingleOrDefaultAsync(cancellationToken) 
            ?? throw new EntityNotFoundException($"Group {groupId} not found");

            if (!group.GroupUsers.Any(gu => gu.UserId == userId))
            {
                throw new BadRequestException($"User {userId} has not been invited to group {groupId}");
            }

            group.GroupUsers = group.GroupUsers.Where(gu => gu.UserId != userId).ToList();

            await this.context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Guid> DeleteUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            var groupUser = await GetGroupUserAsync(this.groupContextProvider.GroupId, userId, cancellationToken);
            this.context.GroupUsers.Remove(groupUser);
            await this.context.SaveChangesAsync(cancellationToken);

            return groupUser.Id;
        }

        public async Task<Guid?> JoinGroupAsync(CancellationToken cancellationToken)
        {
            var groupUser = await GetGroupUserAsync(this.groupContextProvider.GroupId, this.userContextProvider.UserId, cancellationToken);
            groupUser.HasJoined = true;
            await this.context.SaveChangesAsync(cancellationToken);

            return groupUser.Id;
        }

        public async Task<Guid?> LeaveGroupAsync(CancellationToken cancellationToken)
        {
            var groupUser = await GetGroupUserAsync(this.groupContextProvider.GroupId, this.userContextProvider.UserId, cancellationToken);
            this.context.GroupUsers.Remove(groupUser);
            await this.context.SaveChangesAsync(cancellationToken);

            return groupUser.Id;
        }

        private static Expression<Func<GroupUser, GroupUser>> GroupUser_FromEF =>
            (GroupUser gu) => new GroupUser
            {
                Id = gu.Id,
                Group = gu.Group,
                GroupId = gu.GroupId,
                User = gu.User,
                UserId = gu.UserId,
                Applications = gu.Applications,
                Roles = gu.Roles,
                HasJoined = gu.HasJoined,
            };
    }
}
