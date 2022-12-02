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
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Models.Responses.Common;
using Timeboxed.Api.Services.Interfaces;
using Timeboxed.Core.AccessControl.Interfaces;
using Timeboxed.Core.FunctionWrappers;
using Timeboxed.Data.Constants;

namespace Timeboxed.Api.Controllers
{
    public class RoleController
    {
        private readonly ILogger<RoleController> logger;
        private readonly IHttpRequestWrapper<int> httpRequestWrapper;
        private readonly IGroupValidator groupValidationWrapper;
        private readonly IRoleService roleService;

        public RoleController(
            ILogger<RoleController> logger,
            IHttpRequestWrapper<int> httpRequestWrapper,
            IGroupValidator groupValidationWrapper,
            IRoleService roleService)
        {
            this.logger = logger;
            this.httpRequestWrapper = httpRequestWrapper;
            this.groupValidationWrapper = groupValidationWrapper;
            this.roleService = roleService;
        }

        [FunctionName("GetRoles")]
        public async Task<ActionResult<ListResponse<RoleResponse>>> GetRoles(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "roles")] HttpRequest req,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.httpRequestWrapper.ExecuteAsync(
                new List<int> { TimeboxedPermissions.ApplicationAccess },
                async () => new OkObjectResult(await this.roleService.GetRolesAsync(
                    req.Query.TryGetValue("groupId", out var groupId) && Guid.TryParse(groupId, out var groupIdGuid) 
                        ? groupIdGuid 
                        : null,
                    cancellationToken)),
                cancellationToken);

        [FunctionName("GetRoleById")]
        public async Task<ActionResult<RoleResponse>> GetRoleById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "roles/{roleId}")] HttpRequest req,
            string roleId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.httpRequestWrapper.ExecuteAsync(
                new List<int> { TimeboxedPermissions.ApplicationAccess },
                async () => Guid.TryParse(roleId, out Guid roleIdGuid) 
                    ? new OkObjectResult(await this.roleService.GetRoleByIdAsync(roleIdGuid, cancellationToken))
                    : new BadRequestObjectResult(new { message = "Role ID supplied is not a valid GUID"}),
                cancellationToken);

        [FunctionName("CreateRole")]
        public async Task<ActionResult> CreateRole(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "roles")] HttpRequest req,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.httpRequestWrapper.ExecuteAsync(
                new List<int> { TimeboxedPermissions.ApplicationAccess },
                async () =>
                {
                    var requestBody = await ConstructRequestModelAsync<AddRoleRequest>(req);

                    if (requestBody.Name == null)
                    {
                        return new BadRequestObjectResult(new { message = "Invalid role body supplied." });
                    }

                    if (await this.roleService.RoleNameExistsAsync(requestBody.Name, cancellationToken))
                    {
                        return new BadRequestObjectResult(new { message = "Role name already exists." });
                    }

                    var role = await this.roleService.CreateRoleAsync(requestBody, cancellationToken);

                    return new CreatedAtRouteResult(nameof(this.GetRoleById), role.Id, role);
                },
                cancellationToken);

        [FunctionName("DeleteRole")]
        public async Task<ActionResult> DeleteRole(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "roles/{roleId}")] HttpRequest req,
            string roleId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.httpRequestWrapper.ExecuteAsync(
                new List<int> { TimeboxedPermissions.ApplicationAccess },
                async () =>
                {
                    if (!Guid.TryParse(roleId, out var roleIdGuid))
                    {
                        return new BadRequestObjectResult(new { message = "Role ID supplied is not a valid GUID." });
                    }

                    return new OkObjectResult(new { roleId = await this.roleService.DeleteRoleAsync(roleIdGuid, cancellationToken) });
                },
                cancellationToken);

        protected static async Task<T> ConstructRequestModelAsync<T>(HttpRequest req)
        {
            var content = await new StreamReader(req.Body).ReadToEndAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
