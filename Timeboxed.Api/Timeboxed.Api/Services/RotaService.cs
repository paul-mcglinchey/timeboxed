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
using Timeboxed.Core.Exceptions;
using Timeboxed.Core.Extensions;
using Timeboxed.Data;
using Timeboxed.Domain.Models;

namespace Timeboxed.Api.Services
{
    public class RotaService : IRotaService
    {
        private readonly TimeboxedContext context;
        private readonly IUserContextProvider userContextProvider;
        private readonly IGroupContextProvider groupContextProvider;

        public RotaService(
            TimeboxedContext context,
            IUserContextProvider userContextProvider,
            IGroupContextProvider groupContextProvider)
        {
            this.context = context;
            this.userContextProvider = userContextProvider;
            this.groupContextProvider = groupContextProvider;
        }

        public async Task<ListResponse<RotaResponse>> GetRotasAsync(GetRotasRequest requestParameters, CancellationToken cancellationToken)
        {
            var rotaQuery = this.context.Rotas
                .Include(r => r.Employees)
                .Include(r => r.Schedules)
                .Where(r => Guid.Equals(this.groupContextProvider.GroupId, r.GroupId));

            rotaQuery = ApplyFilter(rotaQuery, requestParameters);
            rotaQuery = ApplySort(rotaQuery, requestParameters);

            var count = rotaQuery.Count();
            rotaQuery = rotaQuery.Paginate(requestParameters.PageNumber ?? 1, requestParameters.PageSize ?? 10);

            return new ListResponse<RotaResponse>
            {
                Items = await rotaQuery.Select<Rota, RotaResponse>(r => r).ToListAsync(cancellationToken),
                Count = count,
            };
        }

        public async Task<RotaResponse> AddRotaAsync(AddEditRotaRequest requestBody, CancellationToken cancellationToken)
        {
            var rotaId = Guid.NewGuid();

            var rota = new Rota
            {
                Id = rotaId,
                GroupId = this.groupContextProvider.GroupId,
                Name = requestBody.Name,
                Description = requestBody.Description,
                Colour = requestBody.Colour,
                ClosingHour = requestBody.ClosingHour,
                Employees = requestBody.Employees.Select(e => new RotaEmployee { EmployeeId = e, RotaId = rotaId, }).ToList(),
            };

            rota.AddTracking(this.userContextProvider.UserId, true);

            this.context.Rotas.Add(rota);
            await this.context.SaveChangesAsync(cancellationToken);

            return rota;
        }

        public async Task<RotaResponse> UpdateRotaAsync(Guid rotaId, AddEditRotaRequest requestBody, CancellationToken cancellationToken)
        {
            var rota = await this.context.Rotas
                .Where(r => Guid.Equals(r.Id, rotaId) && Guid.Equals(r.GroupId, this.groupContextProvider.GroupId))
                .Include(c => c.Employees)
                .Include(c => c.Schedules)
                .SingleOrDefaultAsync(cancellationToken);

            rota.AddTracking(this.userContextProvider.UserId);

            rota.Name = requestBody.Name;
            rota.Description = requestBody.Description;
            rota.ClosingHour = requestBody.ClosingHour;
            rota.Colour = requestBody.Colour;
            rota.Locked = requestBody.Locked;
            rota.Employees = requestBody.Employees.Select(e => new RotaEmployee { RotaId = rota.Id, EmployeeId = e }).ToList();

            await this.context.SaveChangesAsync(cancellationToken);

            return rota;
        }

        public async Task<Guid> DeleteRotaAsync(Guid rotaId, CancellationToken cancellationToken)
        {
            var rota = await this.context.Rotas.Where(r => Guid.Equals(r.Id, rotaId)).SingleOrDefaultAsync(cancellationToken);
            this.context.Rotas.Remove(rota);
            await this.context.SaveChangesAsync(cancellationToken);

            return rota.Id;
        }

        public async Task LockRotaAsync(Guid rotaId, CancellationToken cancellationToken)
        {
            var rota = await this.context.Rotas.Where(r => r.Id == rotaId).SingleOrDefaultAsync(cancellationToken) ?? throw new EntityNotFoundException($"Rota {rotaId} not found");
            rota.Locked = true;
            await this.context.SaveChangesAsync(cancellationToken);
        }

        public async Task UnlockRotaAsync(Guid rotaId, CancellationToken cancellationToken)
        {
            var rota = await this.context.Rotas.Where(r => r.Id == rotaId).SingleOrDefaultAsync(cancellationToken) ?? throw new EntityNotFoundException($"Rota {rotaId} not found");
            rota.Locked = false;
            await this.context.SaveChangesAsync(cancellationToken);
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
