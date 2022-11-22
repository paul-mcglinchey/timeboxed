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
using Timeboxed.Core.Extensions;
using Timeboxed.Data;
using Timeboxed.Domain.Models;

namespace Timeboxed.Api.Services
{
    public class RotaService : IRotaService
    {
        private readonly IMapper mapper;
        private readonly TimeboxedContext context;
        private readonly IUserContextProvider userContextProvider;
        private readonly IGroupContextProvider groupContextProvider;

        public RotaService(
            IMapper mapper,
            TimeboxedContext context,
            IUserContextProvider userContextProvider,
            IGroupContextProvider groupContextProvider)
        {
            this.mapper = mapper;
            this.context = context;
            this.userContextProvider = userContextProvider;
            this.groupContextProvider = groupContextProvider;
        }

        public async Task<ListResponse<RotaDto>> GetRotasAsync(GetRotasRequest requestParameters, CancellationToken cancellationToken)
        {
            var rotaQuery = this.context.Rotas
                .Include(r => r.Employees)
                .Include(r => r.Schedules)
                .Where(r => Guid.Equals(this.groupContextProvider.GroupId, r.GroupId));

            rotaQuery = ApplyFilter(rotaQuery, requestParameters);
            rotaQuery = ApplySort(rotaQuery, requestParameters);

            var count = rotaQuery.Count();
            rotaQuery = rotaQuery.Paginate(requestParameters.PageNumber ?? 1, requestParameters.PageSize ?? 10);

            return new ListResponse<RotaDto>
            {
                Items = this.mapper.Map<List<RotaDto>>(await rotaQuery.ToListAsync(cancellationToken)),
                Count = count,
            };
        }

        public async Task<RotaDto> CreateRotaAsync(RotaDto requestBody, CancellationToken cancellationToken)
        {
            var rota = this.mapper.Map<Rota>(requestBody);

            rota.GroupId = this.groupContextProvider.GroupId;
            rota.AddTracking(this.userContextProvider.UserId, true);

            var rotaEmployees = new List<RotaEmployee>(requestBody.Employees.Select(e => new RotaEmployee { RotaId = rota.Id, EmployeeId = e }));

            rota.Employees = rotaEmployees;

            this.context.Rotas.Add(rota);
            await this.context.SaveChangesAsync(cancellationToken);

            return this.mapper.Map<RotaDto>(rota);
        }

        public async Task<RotaDto> UpdateRotaAsync(Guid rotaIdGuid, RotaDto requestBody, CancellationToken cancellationToken)
        {
            var rota = await this.context.Rotas
                .Where(r => Guid.Equals(r.Id, rotaIdGuid) && Guid.Equals(r.GroupId, this.groupContextProvider.GroupId))
                .Include(c => c.Employees)
                .Include(c => c.Schedules)
                .SingleOrDefaultAsync(cancellationToken);

            rota.AddTracking(this.userContextProvider.UserId);

            var rotaEmployees = new List<RotaEmployee>(requestBody.Employees.Select(e => new RotaEmployee { RotaId = rota.Id, EmployeeId = e }));

            rota.Name = requestBody.Name;
            rota.Description = requestBody.Description;
            rota.ClosingHour = requestBody.ClosingHour;
            rota.Colour = requestBody.Colour;
            rota.Locked = requestBody.Locked;
            rota.Employees = rotaEmployees;

            await this.context.SaveChangesAsync(cancellationToken);

            return this.mapper.Map<RotaDto>(rota);
        }

        public async Task<Guid> DeleteRotaAsync(Guid rotaIdGuid, CancellationToken cancellationToken)
        {
            var rota = await this.context.Rotas.Where(r => Guid.Equals(r.Id, rotaIdGuid)).SingleOrDefaultAsync(cancellationToken);
            this.context.Rotas.Remove(rota);
            await this.context.SaveChangesAsync(cancellationToken);

            return rota.Id;
        }

        private static IQueryable<Rota> ApplyFilter(IQueryable<Rota> query, GetRotasRequest request) => query;

        private IQueryable<Rota> ApplySort(IQueryable<Rota> query, GetRotasRequest request)
        {
            return request.SortField switch
            {
                "updatedAt" => request.IsAscending
                    ? query.OrderBy(q => q.UpdatedAt)
                    : query.OrderByDescending(q => q.UpdatedAt),

                _ => request.IsAscending
                    ? query.OrderBy(q => q.Name)
                    : query.OrderByDescending(q => q.Employees.Count),
            };
        }
    }
}
