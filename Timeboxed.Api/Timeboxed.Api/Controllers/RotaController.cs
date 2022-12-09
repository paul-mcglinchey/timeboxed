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
    public class RotaController : GroupControllerWrapper<RotaController>
    {
        private readonly IRotaService rotaService;

        public RotaController(
            ILogger<RotaController> logger,
            IHttpRequestWrapper<int> httpRequestWrapper,
            IGroupValidator groupValidator,
            IRotaService rotaService)
            : base(logger, httpRequestWrapper, groupValidator)
        {
            this.rotaService = rotaService;
        }

        [FunctionName("GetRotas")]
        public async Task<ActionResult<ListResponse<RotaResponse>>> GetRotas(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "groups/{groupId}/rotas")] HttpRequest req,
            string groupId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<int> { TimeboxedPermissions.ViewRotas },
                groupId,
                async () =>
                {
                    var requestParameters = req.DeserializeQueryParams<GetRotasRequest>();

                    if (requestParameters == null)
                    {
                        return new BadRequestResult();
                    }

                    return new OkObjectResult(await this.rotaService.GetRotasAsync(requestParameters, cancellationToken));
                },
                cancellationToken);

        [FunctionName("GetRotaById")]
        public async Task<ActionResult<RotaResponse>> GetRotaById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "groups/{groupId}/rotas/{rotaId}")] HttpRequest req,
            string groupId,
            string rotaId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<int> { TimeboxedPermissions.ViewRotas },
                groupId,
                async () =>
                {
                    if (rotaId == null || !Guid.TryParse(rotaId, out Guid rotaIdGuid))
                    {
                        return new BadRequestObjectResult(new { message = "Rota ID supplied is not a valid GUID" });
                    }

                    return new OkObjectResult(await this.rotaService.GetRotaByIdAsync(rotaIdGuid, cancellationToken));
                },
                cancellationToken);

        [FunctionName("AddRota")]
        public async Task<ActionResult<RotaResponse>> AddRota(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "groups/{groupId}/rotas")] HttpRequest req,
            string groupId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<int> { TimeboxedPermissions.AddEditDeleteRotas },
                groupId,
                async () =>
                {
                    var requestBody = await ConstructRequestModelAsync<AddEditRotaRequest>(req);

                    if (requestBody.Name == null)
                    {
                        return new BadRequestObjectResult(new { message = "Fields missing from request" });
                    }

                    return new CreatedAtRouteResult("rotas", await this.rotaService.AddRotaAsync(requestBody, cancellationToken));
                },
                cancellationToken);

        [FunctionName("UpdateRota")]
        public async Task<ActionResult> UpdateRota(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "groups/{groupId}/rotas/{rotaId}")] HttpRequest req,
            string groupId,
            string rotaId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<int> { TimeboxedPermissions.AddEditDeleteRotas },
                groupId,
                async () =>
                {
                    var requestBody = await ConstructRequestModelAsync<AddEditRotaRequest>(req);

                    if (rotaId == null || !Guid.TryParse(rotaId, out Guid rotaIdGuid))
                    {
                        return new BadRequestObjectResult(new { message = "Rota ID supplied is not a valid GUID" });
                    }

                    await this.rotaService.UpdateRotaAsync(rotaIdGuid, requestBody, cancellationToken);

                    return new NoContentResult();
                },
                cancellationToken);

        [FunctionName("DeleteRota")]
        public async Task<ActionResult> DeleteRota(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "groups/{groupId}/rotas/{rotaId}")] HttpRequest req,
            string groupId,
            string rotaId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<int> { TimeboxedPermissions.AddEditDeleteRotas },
                groupId,
                async () =>
                {
                    if (rotaId == null || !Guid.TryParse(rotaId, out Guid rotaIdGuid))
                    {
                        return new BadRequestObjectResult(new { message = "Rota ID supplied is not a valid GUID" });
                    }

                    await this.rotaService.DeleteRotaAsync(rotaIdGuid, cancellationToken);

                    return new NoContentResult();
                },
                cancellationToken);

        [FunctionName("LockRota")]
        public async Task<ActionResult> LockRota(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "groups/{groupId}/rotas/{rotaId}/lock")] HttpRequest req,
            string groupId,
            string rotaId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<int> { TimeboxedPermissions.AddEditDeleteRotas },
                groupId,
                async () =>
                {
                    if (rotaId == null || !Guid.TryParse(rotaId, out Guid rotaIdGuid))
                    {
                        return new BadRequestObjectResult(new { message = "Rota ID supplied is not a valid GUID" });
                    }

                    await this.rotaService.LockRotaAsync(rotaIdGuid, cancellationToken);

                    return new NoContentResult();
                },
                cancellationToken);

        [FunctionName("UnlockRota")]
        public async Task<ActionResult> UnlockRota(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "groups/{groupId}/rotas/{rotaId}/unlock")] HttpRequest req,
            string groupId,
            string rotaId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<int> { TimeboxedPermissions.AddEditDeleteRotas },
                groupId,
                async () =>
                {
                    if (rotaId == null || !Guid.TryParse(rotaId, out Guid rotaIdGuid))
                    {
                        return new BadRequestObjectResult(new { message = "Rota ID supplied is not a valid GUID" });
                    }

                    await this.rotaService.UnlockRotaAsync(rotaIdGuid, cancellationToken);

                    return new NoContentResult();
                },
                cancellationToken);

        protected static async Task<T> ConstructRequestModelAsync<T>(HttpRequest req)
        {
            var content = await new StreamReader(req.Body).ReadToEndAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
