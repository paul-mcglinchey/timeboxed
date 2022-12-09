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
        public Task<ListResponse<ScheduleResponse>> GetRotaSchedulesAsync(Guid rotaId, CancellationToken cancellationToken);

        public Task<ScheduleResponse> GetRotaScheduleByIdAsync(Guid rotaId, Guid scheduleId, CancellationToken cancellationToken);

        public Task AddRotaScheduleAsync(Guid rotaId, AddEditScheduleRequest requestBody, CancellationToken cancellationToken);

        public Task UpdateRotaScheduleAsync(Guid rotaId, Guid scheduleId, AddEditScheduleRequest requestBody, CancellationToken cancellationToken);
    }
}
