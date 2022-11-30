using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Models.Responses.Common;
using Timeboxed.Api.Services.Interfaces;
using Timeboxed.Core.AccessControl.Interfaces;
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

        public async Task<ListResponse<ScheduleResponse>> GetRotaSchedulesAsync(Guid rotaIdGuid, CancellationToken cancellationToken)
        {
            var schedules = await this.context.Schedules
                .Where(s => s.RotaId.Equals(rotaIdGuid))
                .Include(s => s.EmployeeSchedules)
                    .ThenInclude(es => es.Shifts)
                .Select<Schedule, ScheduleResponse>(s => s)
                .AsSplitQuery()
                .ToListAsync(cancellationToken);

            return new ListResponse<ScheduleResponse>
            {
                Items = schedules,
                Count = schedules.Count
            };
        }

        public async Task<ScheduleResponse> AddRotaScheduleAsync(Guid rotaIdGuid, AddEditScheduleRequest requestBody, CancellationToken cancellationToken)
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

            return schedule;
        }

        public async Task<ScheduleResponse> UpdateRotaScheduleAsync(Guid rotaIdGuid, Guid scheduleIdGuid, AddEditScheduleRequest requestBody, CancellationToken cancellationToken)
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

            return schedule;
        }
    }
}
