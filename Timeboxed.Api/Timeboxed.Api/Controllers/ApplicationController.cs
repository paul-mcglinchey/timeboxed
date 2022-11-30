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
using Timeboxed.Data.Enums;

namespace Timeboxed.Api.Controllers
{
    public class ApplicationController
    {
        private readonly ILogger<ApplicationController> logger;
        private readonly IHttpRequestWrapper<TimeboxedPermission> httpRequestWrapper;
        private readonly IApplicationService applicationService;

        public ApplicationController(
            ILogger<ApplicationController> logger,
            IHttpRequestWrapper<TimeboxedPermission> httpRequestWrapper,
            IApplicationService applicationService)
        {
            this.logger = logger;
            this.httpRequestWrapper = httpRequestWrapper;
            this.applicationService = applicationService;
        }

        [FunctionName("GetApplications")]
        public async Task<ActionResult<ListResponse<ApplicationListResponse>>> GetRoles(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "applications")] HttpRequest req,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.httpRequestWrapper.ExecuteAsync(
                new List<TimeboxedPermission> { TimeboxedPermission.ApplicationAccess },
                async () => new OkObjectResult(await this.applicationService.GetApplicationsAsync(cancellationToken)),
                cancellationToken);
    }
}
