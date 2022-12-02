using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
        private readonly IUserValidator userValidator;

        public GroupUserController(
            ILogger<GroupUserController> logger,
            IHttpRequestWrapper<int> httpRequestWrapper,
            IGroupUserService groupUserService,
            IGroupValidator groupValidator,
            IUserValidator userValidator)
            : base(logger, httpRequestWrapper, groupValidator)
        {
            this.groupUserService = groupUserService;
            this.userValidator = userValidator;
        }

        [FunctionName("GetGroupUsers")]
        public async Task<ActionResult<ListResponse<UserListResponse>>> GetGroupUsers(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "groups/{groupId}/users")] HttpRequest req,
            string groupId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<int> { TimeboxedPermissions.GroupAccess },
                groupId,
                async () => new OkObjectResult(await this.groupUserService.GetGroupUsersAsync(cancellationToken)),
                cancellationToken);

        [FunctionName("UpdateGroupUser")]
        public async Task<ActionResult> UpdateGroupUser(
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

                    if (!this.userValidator.TryValidate(userId, out var userIdGuid))
                    {
                        return new BadRequestObjectResult(new { message = "Invalid user ID supplied." });
                    };

                    return new OkObjectResult(await this.groupUserService.UpdateGroupUserAsync(userIdGuid, request, cancellationToken));
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

                    return new OkObjectResult(await this.groupUserService.InviteGroupUserAsync(request.UsernameOrEmail, cancellationToken));
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
                    if (!this.userValidator.TryValidate(userId, out var userIdGuid))
                    {
                        return new BadRequestObjectResult(new { message = "Invalid user ID supplied." });
                    };

                    return new OkObjectResult(new { groupUserId = await this.groupUserService.DeleteUserAsync(userIdGuid, cancellationToken) });
                },
                cancellationToken);

        [FunctionName("JoinGroup")]
        public async Task<ActionResult> JoinGroup(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "groups/{groupId}/join")] HttpRequest req,
            string groupId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<int> { TimeboxedPermissions.GroupAdminAccess },
                groupId,
                async () => new OkObjectResult(new { groupUserId = await this.groupUserService.JoinGroupAsync(cancellationToken) }),
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
                async () => new OkObjectResult(new { groupUserId = await this.groupUserService.LeaveGroupAsync(cancellationToken) }),
                cancellationToken);

        protected static async Task<T> ConstructRequestModelAsync<T>(HttpRequest req)
        {
            var content = await new StreamReader(req.Body).ReadToEndAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
