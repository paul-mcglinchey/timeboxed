using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
    public class ScheduleController : GroupControllerBase<ScheduleController>
    {
        private readonly IScheduleService scheduleService;

        public ScheduleController(
            ILogger<ScheduleController> logger,
            IHttpRequestWrapper<TimeboxedPermission> httpRequestWrapper,
            IMapper mapper,
            IScheduleService scheduleService,
            IGroupValidator groupValidator)
            : base(logger, httpRequestWrapper, mapper, groupValidator)
        {
            this.scheduleService = scheduleService;
        }

        [FunctionName("GetRotaSchedules")]
        public async Task<ActionResult<ListResponse<ScheduleDto>>> GetRotaSchedules(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "groups/{groupId}/rotas/{rotaId}/schedules")] HttpRequest req,
            string groupId,
            string rotaId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<TimeboxedPermission> { TimeboxedPermission.ViewRotas },
                groupId,
                async () =>
                {
                    if (!Guid.TryParse(rotaId, out var rotaIdGuid))
                    {
                        return new BadRequestObjectResult(new { message = "Rota ID supplied is not a valid GUID" });
                    }

                    return new OkObjectResult(await this.scheduleService.GetRotaSchedulesAsync(rotaIdGuid, cancellationToken));
                },
                cancellationToken);

        [FunctionName("CreateRotaSchedule")]
        public async Task<ActionResult<ScheduleDto>> CreateRotaSchedule(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "groups/{groupId}/rotas/{rotaId}/schedules")] HttpRequest req,
            string groupId,
            string rotaId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<TimeboxedPermission> { TimeboxedPermission.AddEditDeleteRotas },
                groupId,
                async () =>
                {
                    var requestBody = await req.ConstructRequestModelAsync<ScheduleDto>();

                    if (!Guid.TryParse(rotaId, out var rotaIdGuid))
                    {
                        return new BadRequestObjectResult(new { message = "Rota ID supplied is not a valid GUID" });
                    }

                    return new OkObjectResult(await this.scheduleService.CreateRotaScheduleAsync(rotaIdGuid, requestBody, cancellationToken));
                },
                cancellationToken);

        [FunctionName("UpdateRotaSchedule")]
        public async Task<ActionResult<ScheduleDto>> UpdateRotaSchedule(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "groups/{groupId}/rotas/{rotaId}/schedules/{scheduleId}")] HttpRequest req,
            string groupId,
            string rotaId,
            string scheduleId,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.ExecuteAsync(
                new List<TimeboxedPermission> { TimeboxedPermission.AddEditDeleteRotas },
                groupId,
                async () =>
                {
                    var requestBody = await req.ConstructRequestModelAsync<ScheduleDto>();

                    if (!Guid.TryParse(rotaId, out var rotaIdGuid))
                    {
                        return new BadRequestObjectResult(new { message = "Rota ID supplied is not a valid GUID" });
                    }

                    if (!Guid.TryParse(scheduleId, out var scheduleIdGuid))
                    {
                        return new BadRequestObjectResult(new { message = "Schedule ID supplied is not a valid GUID" });
                    }

                    return new OkObjectResult(await this.scheduleService.UpdateRotaScheduleAsync(rotaIdGuid, scheduleIdGuid, requestBody, cancellationToken));
                },
                cancellationToken);
    }
}
