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
using Timeboxed.Core.Extensions;
using Timeboxed.Core.FunctionWrappers;
using Timeboxed.Data.Enums;

namespace Timeboxed.Api.Controllers
{
    public class ClientController : GroupControllerBase<ClientController>
    {
        private readonly IClientService clientService;

        public ClientController(
            ILogger<ClientController> logger,
            IHttpRequestWrapper<TimeboxedPermission> httpRequestWrapper,
            IMapper mapper,
            IClientService clientService,
            IGroupValidator groupValidator)
            : base(logger, httpRequestWrapper, mapper, groupValidator)
        {
            this.clientService = clientService;
        }

        [FunctionName("GetClients")]
        public async Task<ActionResult<ListResponse<ClientListResponse>>> GetClients(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "groups/{groupId}/clients")] HttpRequest req,
            string groupId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<TimeboxedPermission> { TimeboxedPermission.ViewClients },
                groupId,
                async () =>
                {
                    var requestParameters = req.DeserializeQueryParams<GetClientsRequest>();

                    if (requestParameters == null)
                    {
                        return new BadRequestResult();
                    }

                    return new OkObjectResult(await this.clientService.GetClientsAsync(requestParameters, cancellationToken));
                },
                cancellationToken);

        [FunctionName("GetClientById")]
        public async Task<ActionResult<ClientResponse>> GetClientById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "groups/{groupId}/clients/{clientId}")] HttpRequest req,
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

                    return new OkObjectResult(await this.clientService.GetClientByIdAsync(clientIdGuid, cancellationToken));
                },
                cancellationToken);

        [FunctionName("AddClient")]
        public async Task<ActionResult<ListResponse<ClientListResponse>>> AddClient(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "groups/{groupId}/clients")] HttpRequest req,
            string groupId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<TimeboxedPermission> { TimeboxedPermission.AddEditDeleteClients },
                groupId,
                async () =>
                {
                    var requestBody = await req.ConstructRequestModelAsync<AddClientRequest>();
                    var requestParameters = req.DeserializeQueryParams<GetClientsRequest>();

                    if (requestBody.FirstName == null || requestBody.LastName == null || requestBody.PrimaryEmailAddress == null)
                    {
                        return new BadRequestObjectResult(new { message = "Fields missing from request" });
                    }

                    var client = await this.clientService.AddClientAsync(requestBody, cancellationToken);

                    return new CreatedAtRouteResult(
                        nameof(this.GetClientById),
                        new { groupId = client.GroupId.ToString(), clientId = client.Id.ToString() },
                        await this.clientService.GetClientsAsync(requestParameters, cancellationToken));
                },
                cancellationToken);

        [FunctionName("UpdateClient")]
        public async Task<ActionResult<ClientResponse>> UpdateClient(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "groups/{groupId}/clients/{clientId}")] HttpRequest req,
            string groupId,
            string clientId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<TimeboxedPermission> { TimeboxedPermission.AddEditDeleteClients },
                groupId,
                async () =>
                {
                    var requestBody = await ConstructRequestModelAsync<UpdateClientRequest>(req);

                    if (clientId == null || !Guid.TryParse(clientId, out Guid clientIdGuid))
                    {
                        return new BadRequestObjectResult(new { message = "Client ID supplied is not a valid GUID" });
                    }

                    return new OkObjectResult(await this.clientService.UpdateClientAsync(clientIdGuid, requestBody, cancellationToken));
                },
                cancellationToken);

        [FunctionName("DeleteClient")]
        public async Task<ActionResult<ListResponse<ClientResponse>>> DeleteClient(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "groups/{groupId}/clients/{clientId}")] HttpRequest req,
            string groupId,
            string clientId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<TimeboxedPermission> { TimeboxedPermission.AddEditDeleteClients },
                groupId,
                async () =>
                {
                    var requestParameters = req.DeserializeQueryParams<GetClientsRequest>();

                    if (clientId == null || !Guid.TryParse(clientId, out Guid clientIdGuid))
                    {
                        return new BadRequestObjectResult(new { message = "Client ID supplied is not a valid GUID" });
                    }

                    await this.clientService.DeleteClientAsync(clientIdGuid, cancellationToken);

                    return new OkObjectResult(await this.clientService.GetClientsAsync(requestParameters, cancellationToken));
                },
                cancellationToken);

        protected static async Task<T> ConstructRequestModelAsync<T>(HttpRequest req)
        {
            var content = await new StreamReader(req.Body).ReadToEndAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
