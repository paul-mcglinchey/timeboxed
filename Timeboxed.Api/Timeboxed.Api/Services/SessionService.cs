using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class SessionService : ISessionService
    {
        private readonly TimeboxedContext context;
        private readonly IUserContextProvider userContextProvider;
        private readonly IGroupContextProvider groupContextProvider;

        public SessionService(TimeboxedContext context, IUserContextProvider userContextProvider, IGroupContextProvider groupContextProvider)
        {
            this.context = context;
            this.userContextProvider = userContextProvider;
            this.groupContextProvider = groupContextProvider;
        }

        public async Task<ListResponse<SessionResponse>> GetClientSessionsAsync(Guid clientId, GetSessionsRequest request, CancellationToken cancellationToken)
        {
            var sessionsQuery = this.context.Sessions
                .Where(s => Guid.Equals(s.ClientId, clientId))
                .Include(s => s.Tags)
                .Select(s => new SessionResponse
                {
                    Id = s.Id,
                    Title = s.Title,
                    Description = s.Description,
                    SessionDate = s.SessionDate,
                    Tags = s.Tags.Select(t => new GroupClientTagResponse { Id = t.GroupClientTagId, Value = t.GroupClientTag.Value }).ToList(),
                });

            if (request.TagId != null)
            {
                sessionsQuery = sessionsQuery.Where(s => s.Tags.Any(t => t.Id == request.TagId));    
            } 

            return new ListResponse<SessionResponse>
            {
                Items = await sessionsQuery.ToListAsync(cancellationToken),
                Count = sessionsQuery.Count()
            };
        }

        public async Task<SessionResponse> GetClientSessionByIdAsync(Guid sessionId, CancellationToken cancellationToken)
        {
            return await this.context.Sessions
                .Where(s => s.Id == sessionId)
                .Include(s => s.Tags)
                .Select(s => new SessionResponse 
                { 
                    Id = s.Id,
                    Title = s.Title,
                    Description = s.Description,
                    SessionDate = s.SessionDate,
                    Tags = s.Tags.Select(t => new GroupClientTagResponse { Id = t.GroupClientTagId, Value = t.GroupClientTag.Value }).ToList(),
                })
                .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException($"Session {sessionId} not found");
        }

        public async Task<SessionResponse> AddClientSessionAsync(Guid clientId, AddUpdateSessionRequest request, CancellationToken cancellationToken)
        {
            var client = await this.context.Clients
                .Where(c => c.Id == clientId)
                .AsSplitQuery()
                .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException($"Client {clientId} not found");

            var session = new Session(
                request.Title,
                request.SessionDate,
                clientId,
                this.userContextProvider.UserId);

            // get existing tags
            var existingTags = await this.context.GroupClientTags
                .Where(gct => request.Tags.Select(t => t.Id).Contains(gct.Id))
                .ToListAsync(cancellationToken);

            var existingTagIds = existingTags.Select(t => t.Id).ToList();

            // add new tags to the context
            var newTags = request.Tags.Where(t => !existingTagIds.Contains(t.Id)).ToList();
            this.context.GroupClientTags.AddRange(newTags.Select(nt => new GroupClientTag 
            {
                Id = nt.Id,
                GroupId = this.groupContextProvider.GroupId,
                Value = nt.Value,
            }));

            // add all tags (existing and new) to the session
            var tags = new List<SessionTag>();
            tags.AddRange(existingTags.Select(et => new SessionTag { GroupClientTag = et, SessionId = session.Id }));
            tags.AddRange(newTags.Select(nt => new SessionTag { GroupClientTagId = nt.Id, SessionId = session.Id }));

            session.Tags = tags;

            this.context.Sessions.Add(session);
            await this.context.SaveChangesAsync(cancellationToken);

            return session;
        }

        public async Task<SessionResponse> UpdateClientSessionAsync(Guid sessionId, AddUpdateSessionRequest request, CancellationToken cancellationToken)
        {
            var session = await this.context.Sessions
                .Where(s => s.Id == sessionId)
                .Include(s => s.Tags)
                    .ThenInclude(t => t.GroupClientTag)
                .SingleOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException($"Session {sessionId} not found");

            session.Title = request.Title;
            session.Description = request.Description;
            session.SessionDate = request.SessionDate;
            
            // get existing tags
            var existingTags = await this.context.GroupClientTags
                .Where(gct => request.Tags.Select(t => t.Id).Contains(gct.Id))
                .ToListAsync(cancellationToken);
            var existingTagIds = existingTags.Select(et => et.Id);

            // add new tags to the context
            var newTags = request.Tags.Where(t => !existingTagIds.Contains(t.Id)).ToList();
            this.context.GroupClientTags.AddRange(newTags.Select(nt => new GroupClientTag 
            {
                Id = nt.Id,
                GroupId = this.groupContextProvider.GroupId,
                Value = nt.Value,
            }));

            session.Tags.Clear();

            // add all tags (existing and new) to the session
            var tags = new List<SessionTag>();
            tags.AddRange(existingTags.Select(et => new SessionTag { GroupClientTag = et, SessionId = session.Id }));
            tags.AddRange(newTags.Select(nt => new SessionTag { GroupClientTagId = nt.Id, SessionId = session.Id }));
            
            session.Tags = tags;

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
