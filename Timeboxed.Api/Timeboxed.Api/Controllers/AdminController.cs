using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

namespace Timeboxed.Api.Controllers;

public class AdminController : GroupControllerWrapper<AdminController>
{
    private readonly IHttpRequestWrapper<int> httpRequestWrapper;
    private readonly IAdminGroupService groupService;
    private readonly IAdminUserService userService;

    public AdminController(
        IHttpRequestWrapper<int> httpRequestWrapper,
        IAdminGroupService groupService,
        IAdminUserService userService,
        ILogger<AdminController> logger,
        IGroupValidator groupValidator)
        : base(logger, httpRequestWrapper, groupValidator)
    {
        this.httpRequestWrapper = httpRequestWrapper;
        this.groupService = groupService;
        this.userService = userService;
    }

    [FunctionName("AdminGetGroups")]
    public async Task<ActionResult<ListResponse<GroupResponse>>> AdminGetGroups(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "super/groups")] HttpRequest req,
        ILogger logger,
        CancellationToken cancellationToken) =>
        await this.httpRequestWrapper.ExecuteAsync(
            new List<int> { },
            async () =>
            {
                var request = req.DeserializeQueryParams<AdminGetGroupsRequest>();

                return new OkObjectResult(await this.groupService.GetGroupsAsync(request, cancellationToken));
            },  
            cancellationToken,
            true);

    [FunctionName("AdminGetGroupById")]
    public async Task<ActionResult<ListResponse<GroupResponse>>> AdminGetGroupById(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "super/groups/{groupId}")] HttpRequest req,
        Guid groupId,
        ILogger logger,
        CancellationToken cancellationToken) =>
        await this.httpRequestWrapper.ExecuteAsync(
            new List<int> { },
            async () =>
            {
                return new OkObjectResult(await this.groupService.GetGroupAsync(groupId, cancellationToken));
            },
            cancellationToken,
            true);

    [FunctionName("AdminGetUsers")]
    public async Task<ActionResult<ListResponse<UserResponse>>> AdminGetUsers(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "super/users")] HttpRequest req,
        ILogger logger,
        CancellationToken cancellationToken) =>
        await this.httpRequestWrapper.ExecuteAsync(
            new List<int> { },
            async () =>
            {
                var request = req.DeserializeQueryParams<AdminGetUsersRequest>();

                return new OkObjectResult(await this.userService.GetUsersAsync(request, cancellationToken));
            },  
            cancellationToken,
            true);

    [FunctionName("AdminUpdateGroup")]
        public async Task<ActionResult> UpdateGroup(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "super/groups/{groupId}")] HttpRequest req,
            string groupId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<int> { },
                groupId,
                async () =>
                {
                    var request = await req.ConstructRequestModelAsync<AdminUpdateGroupRequest>();

                    await this.groupService.UpdateGroupAsync(request, cancellationToken);

                    return new NoContentResult();
                },
                cancellationToken,
                true);
}
