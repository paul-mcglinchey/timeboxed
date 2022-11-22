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
using Timeboxed.Api.Controllers.Base;
using Timeboxed.Api.Models;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Services.Interfaces;
using Timeboxed.Core.AccessControl.Interfaces;
using Timeboxed.Core.FunctionWrappers;
using Timeboxed.Data.Enums;

namespace Timeboxed.Api.Controllers
{
    public class SessionController : GroupControllerBase<SessionController>
    {
        private readonly ISessionService sessionService;

        public SessionController(
            ILogger<SessionController> logger,
            IHttpRequestWrapper<TimeboxedPermission> httpRequestWrapper,
            IMapper mapper,
            ISessionService sessionService,
            IGroupValidator groupValidator)
            : base(logger, httpRequestWrapper, mapper, groupValidator)
        {
            this.sessionService = sessionService;
        }

        [FunctionName("GetClientSessions")]
        public async Task<ActionResult<ListResponse<SessionResponse>>> GetClientSessions(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "groups/{groupId}/clients/{clientId}/sessions")] HttpRequest req,
            string groupId,
            string clientId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<TimeboxedPermission> { TimeboxedPermission.ViewClients },
                groupId,
                async () =>
                {
                    if (clientId == null || !Guid.TryParse(clientId, out Guid clientIdGuid))
                    {
                        return new BadRequestObjectResult(new { message = "Client ID supplied is not a valid GUID" });
                    }

                    return new OkObjectResult(await this.sessionService.GetClientSessionsAsync(clientIdGuid, cancellationToken));
                },
                cancellationToken);

        [FunctionName("AddClientSession")]
        public async Task<ActionResult<SessionResponse>> AddClientSession(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "groups/{groupId}/clients/{clientId}/sessions")] HttpRequest req,
            string groupId,
            string clientId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<TimeboxedPermission> { TimeboxedPermission.AddEditDeleteClients },
                groupId,
                async () =>
                {
                    var request = await ConstructRequestModelAsync<AddSessionRequest>(req);

                    if (clientId == null || !Guid.TryParse(clientId, out Guid clientIdGuid))
                    {
                        return new BadRequestObjectResult(new { message = "Client ID supplied is not a valid GUID" });
                    }

                    return new CreatedAtRouteResult("sessions", await this.sessionService.AddClientSessionAsync(clientIdGuid, request, cancellationToken));
                },
                cancellationToken);

        [FunctionName("UpdateClientSession")]
        public async Task<ActionResult<SessionResponse>> UpdateClientSession(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "groups/{groupId}/clients/{clientId}/sessions/{sessionId}")] HttpRequest req,
            string groupId,
            string clientId,
            string sessionId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<TimeboxedPermission> { TimeboxedPermission.AddEditDeleteClients },
                groupId,
                async () =>
                {
                    var requestBody = await ConstructRequestModelAsync<UpdateSessionRequest>(req);

                    if (clientId == null || !Guid.TryParse(clientId, out Guid clientIdGuid))
                    {
                        return new BadRequestObjectResult(new { message = "Client ID supplied is not a valid GUID" });
                    }

                    if (sessionId == null || !Guid.TryParse(sessionId, out Guid sessionIdGuid))
                    {
                        return new BadRequestObjectResult(new { message = "Session ID supplied is not a valid GUID" });
                    }

                    return new OkObjectResult(await this.sessionService.UpdateClientSessionAsync(sessionIdGuid, requestBody, cancellationToken));
                },
                cancellationToken);

        [FunctionName("DeleteClientSession")]
        public async Task<ActionResult<Guid>> DeleteClientSession(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "groups/{groupId}/clients/{clientId}/sessions/{sessionId}")] HttpRequest req,
            string groupId,
            string clientId,
            string sessionId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<TimeboxedPermission> { TimeboxedPermission.AddEditDeleteClients },
                groupId,
                async () =>
                {
                    if (clientId == null || !Guid.TryParse(clientId, out Guid clientIdGuid))
                    {
                        return new BadRequestObjectResult(new { message = "Client ID supplied is not a valid GUID" });
                    }

                    if (sessionId == null || !Guid.TryParse(sessionId, out Guid sessionIdGuid))
                    {
                        return new BadRequestObjectResult(new { message = "Session ID supplied is not a valid GUID" });
                    }

                    return new OkObjectResult(new { sessionId = await this.sessionService.DeleteClientSessionAsync(sessionIdGuid, cancellationToken) });
                },
                cancellationToken);

        protected static async Task<T> ConstructRequestModelAsync<T>(HttpRequest req)
        {
            var content = await new StreamReader(req.Body).ReadToEndAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
