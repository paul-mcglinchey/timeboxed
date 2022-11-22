using System;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models;
using Timeboxed.Api.Models.DTOs;

namespace Timeboxed.Api.Services.Interfaces
{
    public interface IScheduleService
    {
        public Task<ListResponse<ScheduleDto>> GetRotaSchedulesAsync(Guid rotaIdGuid, CancellationToken cancellationToken);

        public Task<ScheduleDto> CreateRotaScheduleAsync(Guid rotaIdGuid, ScheduleDto requestBody, CancellationToken cancellationToken);

        public Task<ScheduleDto> UpdateRotaScheduleAsync(Guid rotaIdGuid, Guid scheduleIdGuid, ScheduleDto requestBody, CancellationToken cancellationToken);
    }
}
