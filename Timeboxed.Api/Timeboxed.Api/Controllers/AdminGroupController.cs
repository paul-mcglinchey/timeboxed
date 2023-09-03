using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
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

public class AdminGroupController : GroupControllerWrapper<AdminGroupController>
{
    private readonly IHttpRequestWrapper<int> httpRequestWrapper;
    private readonly IAdminGroupService groupService;

    public AdminGroupController(
        IHttpRequestWrapper<int> httpRequestWrapper,
        IAdminGroupService groupService,
        ILogger<AdminGroupController> logger,
        IGroupValidator groupValidator)
        : base(logger, httpRequestWrapper, groupValidator)
    {
        this.httpRequestWrapper = httpRequestWrapper;
        this.groupService = groupService;
    }

    [FunctionName("AdminGetGroups")]
    public async Task<ActionResult<ListResponse<GroupResponse>>> AdminGetGroups(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "super/groups")] HttpRequest req,
        ILogger logger,
        CancellationToken cancellationToken) =>
        await this.httpRequestWrapper.ExecuteAsync(
            new List<int> { TimeboxedPermissions.ApplicationAccess },
            async () =>
            {
                var request = req.DeserializeQueryParams<AdminGetGroupsRequest>();

                return new OkObjectResult(await this.groupService.GetGroupsAsync(request, cancellationToken));
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
                new List<int> { TimeboxedPermissions.ApplicationAccess },
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
