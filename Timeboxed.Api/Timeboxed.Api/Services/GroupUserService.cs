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

        public async Task<ListResponse<GroupUserResponse>> GetGroupUsers(GetGroupUsersRequest request, CancellationToken cancellationToken)
        {
            var groupId = this.groupContextProvider.GroupId;

            var groupUsersQuery = this.context.GroupUsers
                .Where(gu => gu.GroupId == groupId)
                .Include(gu => gu.Roles)
                .Include(gu => gu.Applications)
                .Select(MapEFGroupUserToResponse);

            if (request.HasJoined != null)
            {
                groupUsersQuery = groupUsersQuery.Where(gu => gu.HasJoined == request.HasJoined);
            }

            return new ListResponse<GroupUserResponse>
            {
                Items = await groupUsersQuery.ToListAsync(cancellationToken),
                Count = await groupUsersQuery.CountAsync(cancellationToken),
            };
        }

        public async Task<GroupUserResponse> GetGroupUserByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var groupId = this.groupContextProvider.GroupId;

            return await this.context.GroupUsers
                .Where(gu => gu.GroupId == groupId && gu.UserId == userId)
                .Include(gu => gu.Roles)
                .Include(gu => gu.Applications)
                .Select(MapEFGroupUserToResponse)
                .SingleOrDefaultAsync(cancellationToken) 
            ?? throw new EntityNotFoundException($"User {userId} in Group {groupId} not found");
        }

        public async Task UpdateGroupUserAsync(Guid userId, UpdateGroupUserRequest request, CancellationToken cancellationToken)
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
        }

        public async Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            var groupUser = await this.context.GroupUsers
                .Where(gu => gu.GroupId == this.groupContextProvider.GroupId && gu.UserId == this.userContextProvider.UserId)
                .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException($"User {this.userContextProvider.UserId} in Group {this.groupContextProvider.GroupId} not found");

            this.context.GroupUsers.Remove(groupUser);

            await this.context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Guid> InviteGroupUserAsync(string usernameOrEmail, CancellationToken cancellationToken)
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

            groupUser = new GroupUser
            {
                UserId = user.Id,
                HasJoined = false,
                Roles = await this.context.Roles.Where(r => r.Id == Guid.Parse(TimeboxedRoles.GroupMember)).ToListAsync(cancellationToken),
            };

            group.GroupUsers.Add(groupUser);
            await this.context.SaveChangesAsync(cancellationToken);

            return groupUser.UserId;
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

        public async Task JoinGroupAsync(CancellationToken cancellationToken)
        {
            var groupUser = await this.context.GroupUsers
                .Where(gu => gu.GroupId == this.groupContextProvider.GroupId && gu.UserId == this.userContextProvider.UserId)
                .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException($"User {this.userContextProvider.UserId} in Group {this.groupContextProvider.GroupId} not found");

            groupUser.HasJoined = true;
            await this.context.SaveChangesAsync(cancellationToken);
        }

        public async Task LeaveGroupAsync(CancellationToken cancellationToken)
        {
            var groupUser = await this.context.GroupUsers
                .Where(gu => gu.GroupId == this.groupContextProvider.GroupId && gu.UserId == this.userContextProvider.UserId)
                .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException($"User {this.userContextProvider.UserId} in Group {this.groupContextProvider.GroupId} not found");

            this.context.GroupUsers.Remove(groupUser);
            await this.context.SaveChangesAsync(cancellationToken);
        }

        private static Expression<Func<GroupUser, GroupUserResponse>> MapEFGroupUserToResponse =>
            (GroupUser gu) => new GroupUserResponse
            {
                Id = gu.Id,
                GroupId = gu.GroupId,
                UserId = gu.UserId,
                Username = gu.User.Username,
                Email = gu.User.Email,
                Applications = gu.Applications.Select(a => a.Id).ToList(),
                Roles = gu.Roles.Select(r => r.Id).ToList(),
                HasJoined = gu.HasJoined,
            };
    }
}
