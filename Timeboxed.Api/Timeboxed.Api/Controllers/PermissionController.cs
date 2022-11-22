using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models;
using Timeboxed.Api.Models.DTOs;
using Timeboxed.Api.Services.Interfaces;
using Timeboxed.Core.FunctionWrappers;
using Timeboxed.Data.Enums;

namespace Timeboxed.Api.Controllers
{
    public class PermissionController
    {
        private readonly ILogger<PermissionController> logger;
        private readonly IMapper mapper;
        private readonly IHttpRequestWrapper<TimeboxedPermission> httpRequestWrapper;
        private readonly IPermissionService permissionService;

        public PermissionController(
            ILogger<PermissionController> logger,
            IMapper mapper,
            IHttpRequestWrapper<TimeboxedPermission> httpRequestWrapper,
            IPermissionService permissionService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.httpRequestWrapper = httpRequestWrapper;
            this.permissionService = permissionService;
        }

        [FunctionName("GetPermissions")]
        public async Task<ActionResult<ListResponse<PermissionDto>>> GetPermissions(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "permissions")] HttpRequest req,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.httpRequestWrapper.ExecuteAsync(
                new List<TimeboxedPermission> { },
                async () => new OkObjectResult(await this.permissionService.GetPermissionsAsync(cancellationToken)),
                cancellationToken);
    }
}
