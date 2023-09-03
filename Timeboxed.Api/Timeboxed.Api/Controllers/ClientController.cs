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
    public class ClientController : GroupControllerWrapper<ClientController>
    {
        private readonly IClientService clientService;

        public ClientController(
            ILogger<ClientController> logger,
            IHttpRequestWrapper<int> httpRequestWrapper,
            IClientService clientService,
            IGroupValidator groupValidator)
            : base(logger, httpRequestWrapper, groupValidator)
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
                new List<int> { TimeboxedPermissions.ViewClients },
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
                new List<int> { TimeboxedPermissions.ViewClients },
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

        [FunctionName("GetClientSessionTags")]
        public async Task<ActionResult<List<GroupClientTagResponse>>> GetClientSessionTags(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "groups/{groupId}/client-tags")] HttpRequest req,
            string groupId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<int> { TimeboxedPermissions.ViewClients },
                groupId,
                async () => 
                {
                    var tags = await this.clientService.GetGroupClientTagsAsync(cancellationToken);
                    
                    return new OkObjectResult(tags);
                });

        [FunctionName("AddClient")]
        public async Task<ActionResult<ListResponse<ClientListResponse>>> AddClient(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "groups/{groupId}/clients")] HttpRequest req,
            string groupId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<int> { TimeboxedPermissions.AddEditDeleteClients },
                groupId,
                async () =>
                {
                    var requestBody = await req.ConstructRequestModelAsync<AddClientRequest>();

                    if (requestBody.FirstName == null || requestBody.LastName == null || requestBody.PrimaryEmailAddress == null)
                    {
                        return new BadRequestObjectResult(new { message = "Fields missing from request" });
                    }

                    var clientId = await this.clientService.AddClientAsync(requestBody, cancellationToken);

                    return new CreatedAtRouteResult(nameof(this.GetClientById), new { groupId, clientId });
                },
                cancellationToken);

        [FunctionName("UpdateClient")]
        public async Task<ActionResult> UpdateClient(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "groups/{groupId}/clients/{clientId}")] HttpRequest req,
            string groupId,
            string clientId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<int> { TimeboxedPermissions.AddEditDeleteClients },
                groupId,
                async () =>
                {
                    var requestBody = await ConstructRequestModelAsync<UpdateClientRequest>(req);

                    if (clientId == null || !Guid.TryParse(clientId, out Guid clientIdGuid))
                    {
                        return new BadRequestObjectResult(new { message = "Client ID supplied is not a valid GUID" });
                    }

                    await this.clientService.UpdateClientAsync(clientIdGuid, requestBody, cancellationToken);

                    return new NoContentResult();
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
                new List<int> { TimeboxedPermissions.AddEditDeleteClients },
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
