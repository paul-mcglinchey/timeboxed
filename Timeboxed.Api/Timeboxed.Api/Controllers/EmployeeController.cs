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
using Timeboxed.Api.Controllers.Base;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Models.Responses.Common;
using Timeboxed.Api.Services.Interfaces;
using Timeboxed.Core.AccessControl.Interfaces;
using Timeboxed.Core.Extensions;
using Timeboxed.Core.FunctionWrappers;
using Timeboxed.Data.Constants;

namespace Timeboxed.Api.Controllers
{
    public class EmployeeController : GroupControllerWrapper<EmployeeController>
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(
            ILogger<EmployeeController> logger,
            IHttpRequestWrapper<int> httpRequestWrapper,
            IEmployeeService employeeService,
            IGroupValidator groupValidator)
            : base(logger, httpRequestWrapper, groupValidator)
        {
            this.employeeService = employeeService;
        }

        [FunctionName("GetEmployees")]
        public async Task<ActionResult<ListResponse<EmployeeResponse>>> GetEmployees(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "groups/{groupId}/employees")] HttpRequest req,
            string groupId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<int> { TimeboxedPermissions.ViewRotas },
                groupId,
                async () =>
                {
                    var requestParameters = req.DeserializeQueryParams<GetEmployeesRequest>();

                    if (requestParameters == null)
                    {
                        return new BadRequestResult();
                    }

                    return new OkObjectResult(await this.employeeService.GetEmployeesAsync(requestParameters, cancellationToken));
                },
                cancellationToken);

        [FunctionName("AddEmployee")]
        public async Task<ActionResult<EmployeeResponse>> AddEmployee(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "groups/{groupId}/employees")] HttpRequest req,
            string groupId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<int> { TimeboxedPermissions.AddEditDeleteRotas },
                groupId,
                async () =>
                {
                    var request = await ConstructRequestModelAsync<AddEmployeeRequest>(req);

                    if (request.FirstName == null || request.LastName == null || request.PrimaryEmailAddress == null)
                    {
                        return new BadRequestObjectResult(new { message = "Fields missing from request" });
                    }

                    return new CreatedAtRouteResult("employees", await this.employeeService.AddEmployeeAsync(request, cancellationToken));
                },
                cancellationToken);

        [FunctionName("UpdateEmployee")]
        public async Task<ActionResult<EmployeeResponse>> UpdateEmployee(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "groups/{groupId}/employees/{employeeId}")] HttpRequest req,
            string groupId,
            string employeeId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<int> { TimeboxedPermissions.AddEditDeleteRotas },
                groupId,
                async () =>
                {
                    var request = await ConstructRequestModelAsync<UpdateEmployeeRequest>(req);

                    if (employeeId == null || !Guid.TryParse(employeeId, out Guid employeeIdGuid))
                    {
                        return new BadRequestObjectResult(new { message = "Employee ID supplied is not a valid GUID" });
                    }

                    return new OkObjectResult(await this.employeeService.UpdateEmployeeAsync(employeeIdGuid, request, cancellationToken));
                },
                cancellationToken);

        [FunctionName("DeleteEmployee")]
        public async Task<ActionResult<Guid>> DeleteEmployee(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "groups/{groupId}/employees/{employeeId}")] HttpRequest req,
            string groupId,
            string employeeId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<int> { TimeboxedPermissions.AddEditDeleteRotas },
                groupId,
                async () =>
                {
                    if (employeeId == null || !Guid.TryParse(employeeId, out Guid employeeIdGuid))
                    {
                        return new BadRequestObjectResult(new { message = "Employee ID supplied is not a valid GUID" });
                    }

                    return new OkObjectResult(new { employeeId = await this.employeeService.DeleteEmployeeAsync(employeeIdGuid, cancellationToken) });
                },
                cancellationToken);

        protected static async Task<T> ConstructRequestModelAsync<T>(HttpRequest req)
        {
            var content = await new StreamReader(req.Body).ReadToEndAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
