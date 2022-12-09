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

        public async Task<ListResponse<RotaListResponse>> GetRotasAsync(GetRotasRequest requestParameters, CancellationToken cancellationToken)
        {
            var rotaQuery = this.context.Rotas
                .Where(r => Guid.Equals(this.groupContextProvider.GroupId, r.GroupId));

            rotaQuery = ApplyFilter(rotaQuery, requestParameters);
            rotaQuery = ApplySort(rotaQuery, requestParameters);

            rotaQuery = rotaQuery.Paginate(requestParameters.PageNumber ?? 1, requestParameters.PageSize ?? 10);

            return new ListResponse<RotaListResponse>
            {
                Items = await rotaQuery.Select(MapEFRotaToListResponse).ToListAsync(cancellationToken),
                Count = rotaQuery.Count(),
            };
        }

        public async Task<RotaResponse> GetRotaByIdAsync(Guid rotaId, CancellationToken cancellationToken) =>
            await this.context.Rotas
                .Where(r => r.Id == rotaId)
                .Select(MapEFRotaToResponse)
                .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException($"Rota {rotaId} not found");
            

        public async Task<Guid> AddRotaAsync(AddEditRotaRequest requestBody, CancellationToken cancellationToken)
        {
            var rota = new Rota
            {
                GroupId = this.groupContextProvider.GroupId,
                Name = requestBody.Name,
                Description = requestBody.Description,
                Colour = requestBody.Colour,
                ClosingHour = requestBody.ClosingHour,
                Employees = await this.context.Employees.Where(e => requestBody.Employees.Contains(e.Id)).ToListAsync(),
            };

            rota.AddTracking(this.userContextProvider.UserId, true);

            this.context.Rotas.Add(rota);
            await this.context.SaveChangesAsync(cancellationToken);

            return rota.Id;
        }

        public async Task UpdateRotaAsync(Guid rotaId, AddEditRotaRequest requestBody, CancellationToken cancellationToken)
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
            rota.Employees = await this.context.Employees.Where(e => requestBody.Employees.Contains(e.Id)).ToListAsync();

            await this.context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteRotaAsync(Guid rotaId, CancellationToken cancellationToken)
        {
            var rota = await this.context.Rotas
                .Where(r => r.Id == rotaId)
                .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException($"Rota {rotaId} not found");

            this.context.Rotas.Remove(rota);
            await this.context.SaveChangesAsync(cancellationToken);
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

        private static Expression<Func<Rota, RotaListResponse>> MapEFRotaToListResponse => (Rota r) => new RotaListResponse
        {
            Id = r.Id,
            GroupId = r.GroupId,
            Name = r.Name,
            Locked = r.Locked,
            Colour = r.Colour,
            Employees = r.Employees.Select(e => e.Id).ToList(),
            UpdatedAt = r.UpdatedAt,
            UpdatedBy = r.UpdatedBy,
            CreatedAt = r.CreatedAt,
            CreatedBy = r.CreatedBy,
        };

        private static Expression<Func<Rota, RotaResponse>> MapEFRotaToResponse => (Rota r) => new RotaResponse
        {
            Id = r.Id,
            GroupId = r.GroupId,
            Name = r.Name,
            Description = r.Description,
            Locked = r.Locked,
            Colour = r.Colour,
            Employees = r.Employees.Select(e => new EmployeeListResponse
            {
                Id = e.Id,
                Role = e.Role,
                GroupId = e.GroupId,
                FirstName = e.FirstName,
                LastName = e.LastName,
                MiddleNames = e.MiddleNames,
                PrimaryEmailAddress = e.PrimaryEmailAddress,
                Holidays = e.Holidays.Select(h => new EmployeeHolidayResponse
                {
                    Id = h.Id,
                    StartDate = h.StartDate,
                    EndDate = h.EndDate,
                    Notes = h.Notes,
                    IsPaid = h.IsPaid
                }).ToList(),
                MinHours = e.MinHours,
                MaxHours = e.MaxHours,
                UnavailableDays = e.UnavailableDays.Select(ud => (DayOfWeek)ud.DayOfWeek).ToList(),
                Colour = e.Colour,
                IsDeleted = e.IsDeleted,
            }).ToList(),
            UpdatedAt = r.UpdatedAt,
            UpdatedBy = r.UpdatedBy,
            CreatedAt = r.CreatedAt,
            CreatedBy = r.CreatedBy,
        };
    }
}
