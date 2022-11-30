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
using Timeboxed.Core.FunctionWrappers;
using Timeboxed.Data.Enums;

namespace Timeboxed.Api.Controllers
{
    public class GroupController : GroupControllerWrapper<GroupController>
    {
        private readonly IGroupService groupService;
        private readonly IGroupContextProvider groupContextProvider;

        public GroupController(
            ILogger<GroupController> logger,
            IHttpRequestWrapper<TimeboxedPermission> httpRequestWrapper,
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
                new List<TimeboxedPermission> { TimeboxedPermission.ApplicationAccess },
                async () => new OkObjectResult(await this.groupService.GetGroupsAsync(cancellationToken)),
                cancellationToken);

        [FunctionName("AddGroup")]
        public async Task<ActionResult> AddGroup(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "groups")] HttpRequest req,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.httpRequestWrapper.ExecuteAsync(
                new List<TimeboxedPermission> { },
                async () =>
                {
                    var request = await ConstructRequestModelAsync<AddGroupRequest>(req);

                    if (await this.groupService.GroupNameExistsAsync(request.Name, cancellationToken))
                    {
                        return new BadRequestObjectResult(new { message = "Group name already exists." });
                    }

                    return new CreatedAtRouteResult("groups", await this.groupService.AddGroupAsync(request, cancellationToken));
                },
                cancellationToken);

        [FunctionName("UpdateGroup")]
        public async Task<ActionResult> UpdateGroup(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "groups/{groupId}")] HttpRequest req,
            string groupId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<TimeboxedPermission> { TimeboxedPermission.GroupAdminAccess },
                groupId,
                async () =>
                {
                    var request = await ConstructRequestModelAsync<UpdateGroupRequest>(req);

                    if (request.Name != null && await this.groupService.GroupNameExistsAsync(this.groupContextProvider.GroupId, request.Name, cancellationToken))
                    {
                        return new BadRequestObjectResult(new { message = "Group name already exists." });
                    }

                    return new OkObjectResult(await this.groupService.UpdateGroupAsync(request, cancellationToken));
                },
                cancellationToken);

        [FunctionName("DeleteGroup")]
        public async Task<ActionResult> DeleteGroup(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "groups/{groupId}")] HttpRequest req,
            string groupId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<TimeboxedPermission> { TimeboxedPermission.GroupAdminAccess },
                groupId,
                async () =>
                {
                    return new OkObjectResult(new { groupId = await this.groupService.DeleteGroupAsync(cancellationToken) });
                },
                cancellationToken);

        protected static async Task<T> ConstructRequestModelAsync<T>(HttpRequest req)
        {
            var content = await new StreamReader(req.Body).ReadToEndAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
