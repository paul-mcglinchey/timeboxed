using System;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Models.Responses.Common;

namespace Timeboxed.Api.Services.Interfaces
{
    public interface IScheduleService
    {
        public Task<ListResponse<ScheduleResponse>> GetRotaSchedulesAsync(Guid rotaIdGuid, CancellationToken cancellationToken);

        public Task<ScheduleResponse> AddRotaScheduleAsync(Guid rotaIdGuid, AddEditScheduleRequest requestBody, CancellationToken cancellationToken);

        public Task<ScheduleResponse> UpdateRotaScheduleAsync(Guid rotaIdGuid, Guid scheduleIdGuid, AddEditScheduleRequest requestBody, CancellationToken cancellationToken);
    }
}
