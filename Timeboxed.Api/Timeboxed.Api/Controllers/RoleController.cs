using AutoMapper;
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
using Timeboxed.Api.Models;
using Timeboxed.Api.Models.DTOs;
using Timeboxed.Api.Services.Interfaces;
using Timeboxed.Core.AccessControl.Interfaces;
using Timeboxed.Core.FunctionWrappers;
using Timeboxed.Data.Enums;
using Timeboxed.Domain.Models;

namespace Timeboxed.Api.Controllers
{
    public class RoleController
    {
        private readonly ILogger<RoleController> logger;
        private readonly IHttpRequestWrapper<TimeboxedPermission> httpRequestWrapper;
        private readonly IGroupValidator groupValidationWrapper;
        private readonly IMapper mapper;
        private readonly IRoleService roleService;

        public RoleController(
            ILogger<RoleController> logger,
            IHttpRequestWrapper<TimeboxedPermission> httpRequestWrapper,
            IGroupValidator groupValidationWrapper,
            IMapper mapper,
            IRoleService roleService)
        {
            this.logger = logger;
            this.httpRequestWrapper = httpRequestWrapper;
            this.groupValidationWrapper = groupValidationWrapper;
            this.mapper = mapper;
            this.roleService = roleService;
        }

        [FunctionName("GetRoles")]
        public async Task<ActionResult<ListResponse<RoleDto>>> GetRoles(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "roles")] HttpRequest req,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.httpRequestWrapper.ExecuteAsync(
                new List<TimeboxedPermission> { TimeboxedPermission.ApplicationAccess },
                async () =>
                    req.Query.TryGetValue("groupId", out var groupId) && Guid.TryParse(groupId, out var groupIdGuid)
                        ? new OkObjectResult(await this.roleService.GetRolesAsync(groupIdGuid, cancellationToken))
                        : new OkObjectResult(await this.roleService.GetRolesAsync(cancellationToken)),
                cancellationToken);

        [FunctionName("CreateRole")]
        public async Task<ActionResult> CreateRole(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "roles")] HttpRequest req,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.httpRequestWrapper.ExecuteAsync(
                new List<TimeboxedPermission> { TimeboxedPermission.ApplicationAccess },
                async () =>
                {
                    var requestBody = await ConstructRequestModelAsync<RoleDto>(req);
                    var role = this.mapper.Map<Role>(requestBody);

                    if (role == null || role.Name == null || role.Description == null)
                    {
                        return new BadRequestObjectResult(new { message = "Invalid role body supplied." });
                    }

                    if (await this.roleService.RoleNameExistsAsync(role.Name, cancellationToken))
                    {
                        return new BadRequestObjectResult(new { message = "Role name already exists." });
                    }

                    var roleId = await this.roleService.CreateRoleAsync(role, cancellationToken);

                    return new CreatedAtRouteResult("roles", new { roleId = roleId });
                },
                cancellationToken);

        [FunctionName("DeleteRole")]
        public async Task<ActionResult> DeleteRole(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "roles/{roleId}")] HttpRequest req,
            string roleId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.httpRequestWrapper.ExecuteAsync(
                new List<TimeboxedPermission> { TimeboxedPermission.ApplicationAccess },
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
