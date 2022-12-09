using System;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Models.Responses.Common;

namespace Timeboxed.Api.Services.Interfaces
{
    public interface IClientService
    {
        public Task<ListResponse<ClientListResponse>> GetClientsAsync(GetClientsRequest requestParameters, CancellationToken cancellationToken);

        public Task<ClientResponse> GetClientByIdAsync(Guid clientId, CancellationToken cancellationToken);

        public Task<Guid> AddClientAsync(AddClientRequest request, CancellationToken cancellationToken);

        public Task UpdateClientAsync(Guid clientId, UpdateClientRequest request, CancellationToken cancellationToken);

        public Task<Guid> DeleteClientAsync(Guid clientId, CancellationToken cancellationToken);
    }
}
