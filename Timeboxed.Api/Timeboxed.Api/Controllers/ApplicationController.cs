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
    public class ApplicationController
    {
        private readonly ILogger<ApplicationController> logger;
        private readonly IHttpRequestWrapper<TimeboxedPermission> httpRequestWrapper;
        private readonly IMapper mapper;
        private readonly IApplicationService applicationService;

        public ApplicationController(
            ILogger<ApplicationController> logger,
            IHttpRequestWrapper<TimeboxedPermission> httpRequestWrapper,
            IMapper mapper,
            IApplicationService applicationService)
        {
            this.logger = logger;
            this.httpRequestWrapper = httpRequestWrapper;
            this.mapper = mapper;
            this.applicationService = applicationService;
        }

        [FunctionName("GetApplications")]
        public async Task<ActionResult<ListResponse<ApplicationDto>>> GetRoles(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "applications")] HttpRequest req,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.httpRequestWrapper.ExecuteAsync(
                new List<TimeboxedPermission> { TimeboxedPermission.ApplicationAccess },
                async () => new OkObjectResult(await this.applicationService.GetApplicationsAsync(cancellationToken)),
                cancellationToken);
    }
}
