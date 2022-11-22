using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models;
using Timeboxed.Api.Services.Interfaces;
using Timeboxed.Core.FunctionWrappers;
using Timeboxed.Data.Enums;

namespace Timeboxed.Api.Controllers
{
    public class MetaInfoController
    {
        private readonly ILogger<MetaInfoController> logger;
        private readonly IHttpRequestWrapper<TimeboxedPermission> httpRequestWrapper;
        private readonly IApplicationService applicationService;
        private readonly IRoleService roleService;
        private readonly IPermissionService permissionService;

        public MetaInfoController(
            IApplicationService applicationService,
            IPermissionService permissionService,
            IRoleService roleService,
            IHttpRequestWrapper<TimeboxedPermission> httpRequestWrapper,
            ILogger<MetaInfoController> logger)
        {
            this.applicationService = applicationService;
            this.permissionService = permissionService;
            this.roleService = roleService;
            this.httpRequestWrapper = httpRequestWrapper;
            this.logger = logger;
        }

        [FunctionName("GetMetaInfo")]
        public async Task<ActionResult<MetaInfo>> GetMetaInfo(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "metainfo")] HttpRequest req,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.httpRequestWrapper.ExecuteAsync(
                new List<TimeboxedPermission> { TimeboxedPermission.ApplicationAccess },
                async () =>
                {
                    var applications = await this.applicationService.GetApplicationsAsync(cancellationToken);
                    var permissions = await this.permissionService.GetPermissionsAsync(cancellationToken);
                    var roles = req.Query.TryGetValue("groupId", out var groupId) && Guid.TryParse(groupId, out var groupIdGuid)
                        ? await this.roleService.GetRolesAsync(groupIdGuid, cancellationToken)
                        : await this.roleService.GetRolesAsync(cancellationToken);

                    return new OkObjectResult(new MetaInfo
                    {
                        Applications = applications.Items,
                        Roles = roles.Items,
                        Permissions = permissions.Items,
                    });
                },
                cancellationToken);
    }
}
