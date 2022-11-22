using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models;
using Timeboxed.Api.Models.DTOs;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Services.Interfaces;
using Timeboxed.Core.AccessControl.Interfaces;
using Timeboxed.Core.Exceptions;
using Timeboxed.Data;
using Timeboxed.Domain.Models;

namespace Timeboxed.Api.Services
{
    public class GroupUserService : IGroupUserService
    {
        private readonly IUserContextProvider userContextProvider;
        private readonly IGroupContextProvider groupContextProvider;
        private readonly TimeboxedContext context;
        private readonly IMapper mapper;

        public GroupUserService(IUserContextProvider userContextProvider, IGroupContextProvider groupContextProvider, TimeboxedContext context, IMapper mapper)
        {
            this.userContextProvider = userContextProvider;
            this.groupContextProvider = groupContextProvider;
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<GroupUser?> GetGroupUserAsync(Guid groupId, Guid userId, CancellationToken cancellationToken)
        {
            return await this.context.GroupUsers
                .Where(gu => gu.GroupId.Equals(groupId) && gu.UserId.Equals(userId))
                .Include(gu => gu.Roles)
                .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<ListResponse<UserDto>> GetGroupUsersAsync(CancellationToken cancellationToken)
        {
            var users = await this.context.Users
                .Where(u => this.context.GroupUsers
                    .Where(gu => gu.GroupId.Equals(this.groupContextProvider.GroupId))
                    .Any(gu => gu.UserId.Equals(u.Id)))
                .ToListAsync(cancellationToken);

            return new ListResponse<UserDto>
            {
                Items = this.mapper.Map<List<UserDto>>(users),
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

        public async Task<Guid> AddUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            var groupUser = new GroupUser { GroupId = this.groupContextProvider.GroupId, UserId = userId };
            this.context.GroupUsers.Add(groupUser);
            await this.context.SaveChangesAsync(cancellationToken);

            return groupUser.Id;
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
