using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models;
using Timeboxed.Api.Models.DTOs;
using Timeboxed.Api.Services.Interfaces;
using Timeboxed.Core.AccessControl.Interfaces;
using Timeboxed.Data;
using Timeboxed.Domain.Models;

namespace Timeboxed.Api.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly TimeboxedContext context;
        private readonly IMapper mapper;
        private readonly IUserContextProvider userContextProvider;
        private readonly IGroupContextProvider groupContextProvider;

        public ScheduleService(TimeboxedContext context, IMapper mapper, IUserContextProvider userContextProvider, IGroupContextProvider groupContextProvider)
        {
            this.context = context;
            this.mapper = mapper;
            this.userContextProvider = userContextProvider;
            this.groupContextProvider = groupContextProvider;
        }

        public async Task<ListResponse<ScheduleDto>> GetRotaSchedulesAsync(Guid rotaIdGuid, CancellationToken cancellationToken)
        {
            var schedules = await this.context.Schedules
                .Where(s => s.RotaId.Equals(rotaIdGuid))
                .Include(s => s.EmployeeSchedules)
                    .ThenInclude(es => es.Shifts)
                .AsSplitQuery()
                .ToListAsync(cancellationToken);

            return new ListResponse<ScheduleDto>
            {
                Items = this.mapper.Map<List<ScheduleDto>>(schedules),
                Count = schedules.Count
            };
        }

        public async Task<ScheduleDto> CreateRotaScheduleAsync(Guid rotaIdGuid, ScheduleDto requestBody, CancellationToken cancellationToken)
        {
            var schedule = this.mapper.Map<Schedule>(requestBody);

            schedule.RotaId = rotaIdGuid;

            this.context.Schedules.Add(schedule);
            await this.context.SaveChangesAsync(cancellationToken);

            return this.mapper.Map<ScheduleDto>(schedule);
        }

        public async Task<ScheduleDto> UpdateRotaScheduleAsync(Guid rotaIdGuid, Guid scheduleIdGuid, ScheduleDto requestBody, CancellationToken cancellationToken)
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
                            Date = s.Date.Value,
                            StartHour = s.StartHour,
                            EndHour = s.EndHour,
                            Notes = s.Notes,
                        }).ToList(),
                })
                .ToList();

            schedule.EmployeeSchedules = schedules;

            await this.context.SaveChangesAsync(cancellationToken);

            return this.mapper.Map<ScheduleDto>(schedule);
        }
    }
}
