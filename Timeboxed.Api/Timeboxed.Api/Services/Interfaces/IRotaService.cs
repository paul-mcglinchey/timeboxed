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
        public Task<ListResponse<RotaResponse>> GetRotasAsync(GetRotasRequest requestParameters, CancellationToken cancellationToken);

        public Task<RotaResponse> AddRotaAsync(AddEditRotaRequest requestBody, CancellationToken cancellationToken);

        public Task<RotaResponse> UpdateRotaAsync(Guid rotaId, AddEditRotaRequest requestBody, CancellationToken cancellationToken);

        public Task<Guid> DeleteRotaAsync(Guid rotaId, CancellationToken cancellationToken);

        public Task LockRotaAsync(Guid rotaId, CancellationToken cancellationToken);

        public Task UnlockRotaAsync(Guid rotaId, CancellationToken cancellationToken);
    }
}
