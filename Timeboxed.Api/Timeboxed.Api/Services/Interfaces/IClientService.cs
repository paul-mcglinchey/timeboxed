using System;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;

namespace Timeboxed.Api.Services.Interfaces
{
    public interface IClientService
    {
        public Task<ListResponse<ClientResponse>> GetClientsAsync(GetClientsRequest requestParameters, CancellationToken cancellationToken);

        public Task<ClientResponse> GetClientByIdAsync(Guid clientId, CancellationToken cancellationToken);

        public Task<ClientResponse> AddClientAsync(AddClientRequest request, CancellationToken cancellationToken);

        public Task<ClientResponse> UpdateClientAsync(Guid clientId, UpdateClientRequest request, CancellationToken cancellationToken);

        public Task<Guid> DeleteClientAsync(Guid clientId, CancellationToken cancellationToken);
    }
}
