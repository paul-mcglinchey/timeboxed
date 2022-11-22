using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Services.Interfaces;
using Timeboxed.Core.AccessControl.Interfaces;
using Timeboxed.Core.Exceptions;
using Timeboxed.Data;
using Timeboxed.Domain.Models;

namespace Timeboxed.Api.Services
{
    public class SessionService : ISessionService
    {
        private readonly TimeboxedContext context;
        private readonly IMapper mapper;
        private readonly IUserContextProvider userContextProvider;

        public SessionService(TimeboxedContext context, IMapper mapper, IUserContextProvider userContextProvider)
        {
            this.context = context;
            this.mapper = mapper;
            this.userContextProvider = userContextProvider;
        }

        public async Task<ListResponse<SessionResponse>> GetClientSessionsAsync(Guid clientId, CancellationToken cancellationToken)
        {
            var sessionsQuery = this.context.Sessions.Where(s => Guid.Equals(s.ClientId, clientId)).Include(s => s.Tags);

            return new ListResponse<SessionResponse>
            {
                Items = this.mapper.Map<List<SessionResponse>>(await sessionsQuery.ToListAsync(cancellationToken)),
                Count = sessionsQuery.Count()
            };
        }

        public async Task<SessionResponse> AddClientSessionAsync(Guid clientId, AddSessionRequest request, CancellationToken cancellationToken)
        {
            var client = await this.context.Clients
                .Where(c => c.Id == clientId)
                .Include(c => c.Sessions)
                .AsSplitQuery()
                .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException($"Client {clientId} not found");

            var session = new Session(
                request.Title,
                request.SessionDate,
                clientId,
                this.userContextProvider.UserId);

            this.context.Sessions.Add(session);

            await this.context.SaveChangesAsync(cancellationToken);

            return session;
        }

        public async Task<SessionResponse> UpdateClientSessionAsync(Guid sessionId, UpdateSessionRequest request, CancellationToken cancellationToken)
        {
            var session = await this.context.Sessions
                .Where(s => s.Id == sessionId)
                .Include(s => s.Tags)
                .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException($"Session {sessionId} not found");

            session.Title = request.Title;
            session.Description = request.Description;
            session.SessionDate = request.SessionDate;
            session.Tags = request.Tags.Select(t => new Tag { Id = t.Id, Value = t.Value }).ToList();

            session.AddTracking(this.userContextProvider.UserId);

            await this.context.SaveChangesAsync(cancellationToken);

            return session;
        }

        public async Task<Guid> DeleteClientSessionAsync(Guid sessionId, CancellationToken cancellationToken)
        {
            var session = await this.context.Sessions.Where(s => Guid.Equals(s.Id, sessionId)).SingleOrDefaultAsync(cancellationToken);

            this.context.Sessions.Remove(session);
            await this.context.SaveChangesAsync(cancellationToken);

            return session.Id;
        }
    }
}
