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
using Timeboxed.Core.FunctionWrappers;
using Timeboxed.Data.Constants;

namespace Timeboxed.Api.Controllers
{
    public class GroupController : GroupControllerWrapper<GroupController>
    {
        private readonly IGroupService groupService;
        private readonly IGroupContextProvider groupContextProvider;

        public GroupController(
            ILogger<GroupController> logger,
            IHttpRequestWrapper<int> httpRequestWrapper,
            IGroupService groupService,
            IGroupContextProvider groupContextProvider,
            IGroupValidator groupValidator)
            : base(logger, httpRequestWrapper, groupValidator)
        {
            this.groupService = groupService;
            this.groupContextProvider = groupContextProvider;
        }

        [FunctionName("GetGroups")]
        public async Task<ActionResult<ListResponse<GroupResponse>>> GetGroups(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "groups")] HttpRequest req,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.httpRequestWrapper.ExecuteAsync(
                new List<int> { TimeboxedPermissions.ApplicationAccess },
                async () => new OkObjectResult(await this.groupService.GetGroupsAsync(cancellationToken)),
                cancellationToken);

        [FunctionName("GetGroupInvites")]
        public async Task<ActionResult<ListResponse<GroupResponse>>> GetGroupInvites(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "groupinvites")] HttpRequest req,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.httpRequestWrapper.ExecuteAsync(
                new List<int> { TimeboxedPermissions.ApplicationAccess },
                async () => new OkObjectResult(await this.groupService.GetGroupInvitesAsync(cancellationToken)),
                cancellationToken);

        [FunctionName("GetGroupById")]
        public async Task<ActionResult<GroupResponse>> GetGroupById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "groups/{groupId}")] HttpRequest req,
            string groupId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.httpRequestWrapper.ExecuteAsync(
                new List<int> { TimeboxedPermissions.ApplicationAccess },
                async () =>
                {
                    if (groupId == null || !Guid.TryParse(groupId, out Guid groupIdGuid))
                    {
                        return new BadRequestObjectResult(new { message = "Group ID supplied is not a valid GUID" });
                    }

                    return new OkObjectResult(await this.groupService.GetGroupByIdAsync(groupIdGuid, cancellationToken));
                },
                cancellationToken);

        [FunctionName("AddGroup")]
        public async Task<ActionResult<GroupResponse>> AddGroup(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "groups")] HttpRequest req,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.httpRequestWrapper.ExecuteAsync(
                new List<int> { },
                async () =>
                {
                    var request = await ConstructRequestModelAsync<AddGroupRequest>(req);

                    if (await this.groupService.GroupNameExistsAsync(request.Name, cancellationToken))
                    {
                        return new BadRequestObjectResult(new { message = "Group name already exists." });
                    }

                    var groupId = await this.groupService.AddGroupAsync(request, cancellationToken);

                    return new CreatedAtRouteResult(nameof(this.GetGroupById), new { groupId = groupId }, await this.groupService.GetGroupByIdAsync(groupId, cancellationToken));
                },
                cancellationToken);

        [FunctionName("UpdateGroup")]
        public async Task<ActionResult<GroupResponse>> UpdateGroup(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "groups/{groupId}")] HttpRequest req,
            string groupId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<int> { TimeboxedPermissions.GroupAdminAccess },
                groupId,
                async () =>
                {
                    var request = await ConstructRequestModelAsync<UpdateGroupRequest>(req);

                    if (request.Name != null && await this.groupService.GroupNameExistsAsync(this.groupContextProvider.GroupId, request.Name, cancellationToken))
                    {
                        return new BadRequestObjectResult(new { message = "Group name already exists." });
                    }

                    await this.groupService.UpdateGroupAsync(request, cancellationToken);

                    return new OkObjectResult(await this.groupService.GetGroupByIdAsync(this.groupContextProvider.GroupId, cancellationToken));
                },
                cancellationToken);

        [FunctionName("DeleteGroup")]
        public async Task<ActionResult> DeleteGroup(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "groups/{groupId}")] HttpRequest req,
            string groupId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<int> { TimeboxedPermissions.GroupAdminAccess },
                groupId,
                async () =>
                {
                    await this.groupService.DeleteGroupAsync(cancellationToken);

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
