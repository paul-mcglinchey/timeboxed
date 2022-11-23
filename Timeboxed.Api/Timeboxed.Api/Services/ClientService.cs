using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Services.Interfaces;
using Timeboxed.Core.AccessControl.Interfaces;
using Timeboxed.Core.Exceptions;
using Timeboxed.Core.Extensions;
using Timeboxed.Data;
using Timeboxed.Domain.Models;

namespace Timeboxed.Api.Services
{
    public class ClientService : IClientService
    {
        private readonly IMapper mapper;
        private readonly TimeboxedContext context;
        private readonly IUserContextProvider userContextProvider;
        private readonly IGroupContextProvider groupContextProvider;

        public ClientService(IMapper mapper, TimeboxedContext context, IUserContextProvider userContextProvider, IGroupContextProvider groupContextProvider)
        {
            this.mapper = mapper;
            this.context = context;
            this.userContextProvider = userContextProvider;
            this.groupContextProvider = groupContextProvider;
        }

        public async Task<ListResponse<ClientListResponse>> GetClientsAsync(GetClientsRequest requestParameters, CancellationToken cancellationToken)
        {
            var clientQuery = this.context.Clients.Where(c => this.groupContextProvider.GroupId == c.GroupId);

            clientQuery = ApplyFilter(clientQuery, requestParameters);
            clientQuery = ApplySort(clientQuery, requestParameters);

            var count = clientQuery.Count();
            clientQuery = clientQuery.Paginate(requestParameters.PageNumber ?? 1, requestParameters.PageSize ?? 10);

            return new ListResponse<ClientListResponse>
            {
                Items = await clientQuery.Select(EFClientToClientListResponse).ToListAsync(cancellationToken),
                Count = count,
            };
        }

        public async Task<ClientResponse> GetClientByIdAsync(Guid clientId, CancellationToken cancellationToken) =>
            await this.context.Clients
                .Where(c => c.Id == clientId && c.GroupId == this.groupContextProvider.GroupId)
                .Include(c => c.Emails)
                .Include(c => c.PhoneNumbers)
                .Include(c => c.Sessions)
                .AsSplitQuery()
                .SingleOrDefaultAsync() 
            ?? throw new EntityNotFoundException($"Client {clientId} not found");

        public async Task<ClientResponse> AddClientAsync(AddClientRequest request, CancellationToken cancellationToken)
        {
            var client = new Client(
                request.FirstName,
                request.LastName,
                request.PrimaryEmailAddress,
                request.Colour,
                this.groupContextProvider.GroupId,
                this.userContextProvider.UserId);

            this.context.Clients.Add(client);
            await this.context.SaveChangesAsync(cancellationToken);

            return client;
        }

        public async Task<ClientResponse> UpdateClientAsync(Guid clientId, UpdateClientRequest request, CancellationToken cancellationToken)
        {
            var client = await this.context.Clients
                .Where(c => c.Id == clientId && c.GroupId == this.groupContextProvider.GroupId)
                .Include(c => c.Emails)
                .Include(c => c.PhoneNumbers)
                .Include(c => c.Sessions)
                .AsSplitQuery()
                .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException($"Client {clientId} not found");

            client.FirstName = request.FirstName;
            client.MiddleNames = request.MiddleNames;
            client.LastName = request.LastName;
            client.PrimaryEmailAddress = request.PrimaryEmailAddress;
            client.PrimaryPhoneNumber = request.PrimaryPhoneNumber;
            client.FirstLine = request.FirstLine;
            client.SecondLine = request.SecondLine;
            client.ThirdLine = request.ThirdLine;
            client.City = request.City;
            client.Country = request.Country;
            client.PostCode = request.PostCode;
            client.ZipCode = request.ZipCode;
            client.BirthDate = request.BirthDate;
            client.Colour = request.Colour;

            await this.context.SaveChangesAsync(cancellationToken);

            return client;
        }

        public async Task<Guid> DeleteClientAsync(Guid clientId, CancellationToken cancellationToken)
        {
            var client = await this.context.Clients
                .Where(c => c.Id == clientId)
                .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException($"Client {clientId} not found");

            this.context.Clients.Remove(client);
            await this.context.SaveChangesAsync(cancellationToken);

            return client.Id;
        }

        private static IQueryable<Client> ApplyFilter(IQueryable<Client> query, GetClientsRequest request)
        {
            if (request.Name?.Trim() != null)
            {
                query = query.Where(q => (q.FirstName + " " + q.LastName).Contains(request.Name));
            }

            if (request.Email?.Trim() != null)
            {
                query = query.Where(q => q.PrimaryEmailAddress.Contains(request.Email));
            }

            return query;
        }

        private IQueryable<Client> ApplySort(IQueryable<Client> query, GetClientsRequest request)
        {
            return request.SortField switch
            {
                "updatedAt" => request.IsAscending
                    ? query.OrderBy(q => q.UpdatedAt)
                    : query.OrderByDescending(q => q.UpdatedAt),
                
                "sessions" => request.IsAscending
                    ? query.OrderBy(q => q.Sessions.Count)
                    : query.OrderByDescending(q => q.Sessions.Count),

                _ => request.IsAscending
                    ? query.OrderBy(q => q.FirstName).ThenBy(q => q.LastName)
                    : query.OrderByDescending(q => q.FirstName).ThenBy(q => q.LastName)

            };
        }

        private static Expression<Func<Client, ClientListResponse>> EFClientToClientListResponse => (Client c) => new ClientListResponse
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            PrimaryEmailAddress = c.PrimaryEmailAddress,
            Sessions = c.Sessions.Select(s => s.Id).ToList(),
            Colour = c.Colour,
            UpdatedAt = c.UpdatedAt,
            UpdatedBy = c.UpdatedBy,
            CreatedAt = c.CreatedAt,
            CreatedBy = c.CreatedBy,
        };
    }
}
