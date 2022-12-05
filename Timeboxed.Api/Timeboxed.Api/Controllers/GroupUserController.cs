using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Controllers.Base;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Models.Responses.Common;
using Timeboxed.Api.Services.Interfaces;
using Timeboxed.Core.AccessControl.Interfaces;
using Timeboxed.Core.Extensions;
using Timeboxed.Core.FunctionWrappers;
using Timeboxed.Data.Constants;

namespace Timeboxed.Api.Controllers
{
    public class GroupUserController : GroupControllerWrapper<GroupUserController>
    {
        private readonly IGroupUserService groupUserService;
        private readonly IGroupService groupService;
        private readonly IGroupContextProvider groupContextProvider;

        public GroupUserController(
            ILogger<GroupUserController> logger,
            IHttpRequestWrapper<int> httpRequestWrapper,
            IGroupUserService groupUserService,
            IGroupService groupService,
            IGroupContextProvider groupContextProvider,
            IGroupValidator groupValidator)
            : base(logger, httpRequestWrapper, groupValidator)
        {
            this.groupUserService = groupUserService;
            this.groupService = groupService;
            this.groupContextProvider = groupContextProvider;
        }

        [FunctionName("GetGroupUserById")]
        public async Task<ActionResult<GroupUserResponse>> GetGroupUserById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "groups/{groupId}/users/{userId}")] HttpRequest req,
            string groupId,
            string userId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<int> { TimeboxedPermissions.GroupAccess },
                groupId,
                async () => 
                {
                    if (!Guid.TryParse(userId, out var userIdGuid))
                    {
                        return new BadRequestObjectResult(new { message = "Invalid user ID supplied" });
                    }

                    return new OkObjectResult(await this.groupUserService.GetGroupUserByIdAsync(userIdGuid, cancellationToken));
                },
                cancellationToken);

        [FunctionName("UpdateGroupUser")]
        public async Task<ActionResult<GroupUserResponse>> UpdateGroupUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "groups/{groupId}/users/{userId}")] HttpRequest req,
            string groupId,
            string userId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<int> { TimeboxedPermissions.GroupAdminAccess },
                groupId,
                async () =>
                {
                    var request = await req.ConstructRequestModelAsync<UpdateGroupUserRequest>();

                    if (!Guid.TryParse(userId, out var userIdGuid))
                    {
                        return new BadRequestObjectResult(new { message = "Invalid user ID supplied" });
                    }

                    await this.groupUserService.UpdateGroupUserAsync(userIdGuid, request, cancellationToken);

                    return new OkObjectResult(await this.groupUserService.GetGroupUserByIdAsync(userIdGuid, cancellationToken));
                },
                cancellationToken);

        [FunctionName("DeleteUser")]
        public async Task<ActionResult> DeleteUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "groups/{groupId}/users/{userId}")] HttpRequest req,
            string groupId,
            string userId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<int> { TimeboxedPermissions.GroupAdminAccess },
                groupId,
                async () =>
                {
                    if (!Guid.TryParse(userId, out var userIdGuid))
                    {
                        return new BadRequestObjectResult(new { message = "Invalid user ID supplied" });
                    }

                    await this.groupUserService.DeleteUserAsync(userIdGuid, cancellationToken);

                    return new NoContentResult();
                },
                cancellationToken);

        [FunctionName("InviteGroupUser")]
        public async Task<ActionResult<ListResponse<UserListResponse>>> InviteGroupUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "groups/{groupId}/users/invite")] HttpRequest req,
            string groupId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<int> { TimeboxedPermissions.GroupAdminAccess },
                groupId,
                async () =>
                {
                    var request = await req.ConstructRequestModelAsync<InviteGroupUserRequest>();

                    var userId = await this.groupUserService.InviteGroupUserAsync(request.UsernameOrEmail, cancellationToken);

                    return new OkObjectResult(await this.groupUserService.GetGroupUserByIdAsync(userId, cancellationToken));
                },
                cancellationToken);

        [FunctionName("UninviteGroupUser")]
        public async Task<ActionResult> UninviteGroupUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "groups/{groupId}/users/{userId}/uninvite")] HttpRequest req,
            string groupId,
            string userId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<int> { TimeboxedPermissions.GroupAdminAccess },
                groupId,
                async () =>
                {
                    if (!Guid.TryParse(userId, out var userIdGuid))
                    {
                        return new BadRequestObjectResult(new { message = "Invalid user ID supplied" });
                    }

                    await this.groupUserService.UninviteGroupUserAsync(userIdGuid, cancellationToken);

                    return new NoContentResult();
                },
                cancellationToken);

        [FunctionName("JoinGroup")]
        public async Task<ActionResult<GroupResponse>> JoinGroup(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "groups/{groupId}/join")] HttpRequest req,
            string groupId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<int> { TimeboxedPermissions.GroupAdminAccess },
                groupId,
                async () =>
                {
                    await this.groupUserService.JoinGroupAsync(cancellationToken);

                    return new OkObjectResult(await this.groupService.GetGroupByIdAsync(this.groupContextProvider.GroupId, cancellationToken));
                },
                cancellationToken);

        [FunctionName("LeaveGroup")]
        public async Task<ActionResult> LeaveGroup(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "groups/{groupId}/leave")] HttpRequest req,
            string groupId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<int> { TimeboxedPermissions.GroupAdminAccess },
                groupId,
                async () =>
                {
                    await this.groupUserService.LeaveGroupAsync(cancellationToken);

                    return new NoContentResult();    
                },
                cancellationToken);

        protected static async Task<T> ConstructRequestModelAsync<T>(HttpRequest req)
        {
            var content = await new StreamReader(req.Body).ReadToEndAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
