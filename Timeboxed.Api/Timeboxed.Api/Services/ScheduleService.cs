using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Models.Responses.Common;
using Timeboxed.Api.Services.Interfaces;
using Timeboxed.Core.AccessControl.Interfaces;
using Timeboxed.Core.Exceptions;
using Timeboxed.Data;
using Timeboxed.Domain.Models;

namespace Timeboxed.Api.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly TimeboxedContext context;
        private readonly IUserContextProvider userContextProvider;
        private readonly IGroupContextProvider groupContextProvider;

        public ScheduleService(TimeboxedContext context, IUserContextProvider userContextProvider, IGroupContextProvider groupContextProvider)
        {
            this.context = context;
            this.userContextProvider = userContextProvider;
            this.groupContextProvider = groupContextProvider;
        }

        public async Task<ListResponse<ScheduleResponse>> GetRotaSchedulesAsync(Guid rotaId, CancellationToken cancellationToken)
        {
            var schedules = await this.context.Schedules
                .Where(s => s.RotaId == rotaId)
                .Select(MapEFScheduleToResponse)
                .AsSplitQuery()
                .ToListAsync(cancellationToken);

            return new ListResponse<ScheduleResponse>
            {
                Items = schedules,
                Count = schedules.Count
            };
        }

        public async Task<ScheduleResponse> GetRotaScheduleByIdAsync(Guid rotaId, Guid scheduleId, CancellationToken cancellationToken) =>
            await this.context.Schedules
                    .Where(s => s.RotaId == rotaId && s.Id == scheduleId)
                    .Select(MapEFScheduleToResponse)
                    .AsSplitQuery()
                    .SingleOrDefaultAsync(cancellationToken)
                ?? throw new EntityNotFoundException($"Schedule {scheduleId} on Rota {rotaId} not found");

        public async Task AddRotaScheduleAsync(Guid rotaIdGuid, AddEditScheduleRequest requestBody, CancellationToken cancellationToken)
        {
            var schedule = new Schedule
            {
                RotaId = rotaIdGuid,
                StartDate = requestBody.StartDate,
                EmployeeSchedules = requestBody.EmployeeSchedules.Select(es => new EmployeeSchedule
                {
                    EmployeeId = es.EmployeeId,
                    Shifts = es.Shifts.Select(s => new EmployeeScheduleShift
                    {
                        Date = s.Date,
                        StartHour = s.StartHour,
                        EndHour = s.EndHour,
                        Notes = s.Notes,
                    }).ToList(),
                }).ToList(),
            };

            this.context.Schedules.Add(schedule);
            await this.context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateRotaScheduleAsync(Guid rotaIdGuid, Guid scheduleIdGuid, AddEditScheduleRequest requestBody, CancellationToken cancellationToken)
        {
            var schedule = await this.context.Schedules
                .Where(s => s.RotaId.Equals(rotaIdGuid) && s.Id.Equals(scheduleIdGuid))
                .Include(s => s.EmployeeSchedules)
                    .ThenInclude(es => es.Shifts)
                .SingleOrDefaultAsync(cancellationToken);

            // remove invalid employee shifts
            var schedules = requestBody
                .EmployeeSchedules
                .Select(es => new EmployeeSchedule 
                { 
                    EmployeeId = es.EmployeeId,
                    Shifts = es.Shifts
                        .Where(s => s != null)
                        .Select(s => new EmployeeScheduleShift 
                        {
                            Date = s.Date,
                            StartHour = s.StartHour,
                            EndHour = s.EndHour,
                            Notes = s.Notes,
                        }).ToList(),
                })
                .ToList();

            schedule.EmployeeSchedules = schedules;
            await this.context.SaveChangesAsync(cancellationToken);
        }

        private static Expression<Func<Schedule, ScheduleResponse>> MapEFScheduleToResponse => (Schedule s) => new ScheduleResponse
        {
            Id = s.Id,
            StartDate = s.StartDate,
            EmployeeSchedules = s.EmployeeSchedules.Select(es => new EmployeeScheduleResponse
            {
                EmployeeId = es.EmployeeId,
                Shifts = es.Shifts.Select(ess => new EmployeeScheduleShiftResponse
                {
                    Date = ess.Date,
                    StartHour = ess.StartHour,
                    EndHour = ess.EndHour,
                    Notes = ess.Notes,
                }).ToList(),
            }).ToList(),
        };
    }
}
