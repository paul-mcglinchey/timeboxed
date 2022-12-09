using System;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Models.Responses.Common;

namespace Timeboxed.Api.Services.Interfaces
{
    public interface IRotaService
    {
        public Task<ListResponse<RotaListResponse>> GetRotasAsync(GetRotasRequest requestParameters, CancellationToken cancellationToken);

        public Task<RotaResponse> GetRotaByIdAsync(Guid rotaId, CancellationToken cancellationToken);

        public Task<Guid> AddRotaAsync(AddEditRotaRequest requestBody, CancellationToken cancellationToken);

        public Task UpdateRotaAsync(Guid rotaId, AddEditRotaRequest requestBody, CancellationToken cancellationToken);

        public Task DeleteRotaAsync(Guid rotaId, CancellationToken cancellationToken);

        public Task LockRotaAsync(Guid rotaId, CancellationToken cancellationToken);

        public Task UnlockRotaAsync(Guid rotaId, CancellationToken cancellationToken);
    }
}
