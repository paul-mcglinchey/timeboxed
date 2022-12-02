using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Models.Responses.Common;
using Timeboxed.Api.Services.Interfaces;
using Timeboxed.Core.FunctionWrappers;

namespace Timeboxed.Api.Controllers
{
    public class PermissionController
    {
        private readonly ILogger<PermissionController> logger;
        private readonly IHttpRequestWrapper<int> httpRequestWrapper;
        private readonly IPermissionService permissionService;

        public PermissionController(
            ILogger<PermissionController> logger,
            IHttpRequestWrapper<int> httpRequestWrapper,
            IPermissionService permissionService)
        {
            this.logger = logger;
            this.httpRequestWrapper = httpRequestWrapper;
            this.permissionService = permissionService;
        }

        [FunctionName("GetPermissions")]
        public async Task<ActionResult<ListResponse<PermissionResponse>>> GetPermissions(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "permissions")] HttpRequest req,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.httpRequestWrapper.ExecuteAsync(
                new List<int> { },
                async () => new OkObjectResult(await this.permissionService.GetPermissionsAsync(cancellationToken)),
                cancellationToken);
    }
}
