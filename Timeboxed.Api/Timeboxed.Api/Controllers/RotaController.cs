﻿using AutoMapper;
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
using Timeboxed.Api.Models.DTOs;
using Timeboxed.Api.Services.Interfaces;
using Timeboxed.Core.AccessControl.Interfaces;
using Timeboxed.Core.Extensions;
using Timeboxed.Core.FunctionWrappers;
using Timeboxed.Data.Enums;

namespace Timeboxed.Api.Controllers
{
    public class RotaController : GroupControllerBase<RotaController>
    {
        private readonly IRotaService rotaService;

        public RotaController(
            ILogger<RotaController> logger,
            IHttpRequestWrapper<TimeboxedPermission> httpRequestWrapper,
            IGroupValidator groupValidator,
            IMapper mapper,
            IRotaService rotaService)
            : base(logger, httpRequestWrapper, mapper, groupValidator)
        {
            this.rotaService = rotaService;
        }

        [FunctionName("GetRotas")]
        public async Task<ActionResult<ListResponse<RotaDto>>> GetRotas(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "groups/{groupId}/rotas")] HttpRequest req,
            string groupId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<TimeboxedPermission> { TimeboxedPermission.ViewRotas },
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

        [FunctionName("CreateRota")]
        public async Task<ActionResult<RotaDto>> CreateRota(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "groups/{groupId}/rotas")] HttpRequest req,
            string groupId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<TimeboxedPermission> { TimeboxedPermission.AddEditDeleteRotas },
                groupId,
                async () =>
                {
                    var requestBody = await ConstructRequestModelAsync<RotaDto>(req);

                    if (requestBody.Name == null)
                    {
                        return new BadRequestObjectResult(new { message = "Fields missing from request" });
                    }

                    return new CreatedAtRouteResult("rotas", await this.rotaService.CreateRotaAsync(requestBody, cancellationToken));
                },
                cancellationToken);

        [FunctionName("UpdateRota")]
        public async Task<ActionResult<RotaDto>> UpdateRota(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "groups/{groupId}/rotas/{rotaId}")] HttpRequest req,
            string groupId,
            string rotaId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<TimeboxedPermission> { TimeboxedPermission.AddEditDeleteRotas },
                groupId,
                async () =>
                {
                    var requestBody = await ConstructRequestModelAsync<RotaDto>(req);

                    if (rotaId == null || !Guid.TryParse(rotaId, out Guid rotaIdGuid))
                    {
                        return new BadRequestObjectResult(new { message = "Rota ID supplied is not a valid GUID" });
                    }

                    return new OkObjectResult(await this.rotaService.UpdateRotaAsync(rotaIdGuid, requestBody, cancellationToken));
                },
                cancellationToken);

        [FunctionName("DeleteRota")]
        public async Task<ActionResult<Guid>> DeleteRota(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "groups/{groupId}/rotas/{rotaId}")] HttpRequest req,
            string groupId,
            string rotaId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<TimeboxedPermission> { TimeboxedPermission.AddEditDeleteRotas },
                groupId,
                async () =>
                {
                    if (rotaId == null || !Guid.TryParse(rotaId, out Guid rotaIdGuid))
                    {
                        return new BadRequestObjectResult(new { message = "Rota ID supplied is not a valid GUID" });
                    }

                    return new OkObjectResult(new { rotaId = await this.rotaService.DeleteRotaAsync(rotaIdGuid, cancellationToken) });
                },
                cancellationToken);

        protected static async Task<T> ConstructRequestModelAsync<T>(HttpRequest req)
        {
            var content = await new StreamReader(req.Body).ReadToEndAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}