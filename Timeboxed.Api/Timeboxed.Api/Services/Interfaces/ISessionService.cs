using System;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;

namespace Timeboxed.Api.Services.Interfaces
{
    public interface ISessionService
    {
        public Task<ListResponse<SessionResponse>> GetClientSessionsAsync(Guid clientId, CancellationToken cancellationToken);

        public Task<SessionResponse> AddClientSessionAsync(Guid clientId, AddSessionRequest request, CancellationToken cancellationToken);

        public Task<SessionResponse> UpdateClientSessionAsync(Guid sessionId, UpdateSessionRequest request, CancellationToken cancellationToken);

        public Task<Guid> DeleteClientSessionAsync(Guid sessionId, CancellationToken cancellationToken);
    }
}
