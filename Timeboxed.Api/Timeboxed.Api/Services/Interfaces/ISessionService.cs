using System;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Models.Responses.Common;

namespace Timeboxed.Api.Services.Interfaces
{
    public interface ISessionService
    {
        public Task<ListResponse<SessionResponse>> GetClientSessionsAsync(Guid clientId, CancellationToken cancellationToken);

        public Task<SessionResponse> GetClientSessionByIdAsync(Guid sessionId, CancellationToken cancellationToken);

        public Task<SessionResponse> AddClientSessionAsync(Guid clientId, AddSessionRequest request, CancellationToken cancellationToken);

        public Task<SessionResponse> UpdateClientSessionAsync(Guid sessionId, UpdateSessionRequest request, CancellationToken cancellationToken);

        public Task<Guid> DeleteClientSessionAsync(Guid sessionId, CancellationToken cancellationToken);
    }
}