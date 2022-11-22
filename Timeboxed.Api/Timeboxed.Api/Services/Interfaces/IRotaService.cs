using System;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models;
using Timeboxed.Api.Models.DTOs;

namespace Timeboxed.Api.Services.Interfaces
{
    public interface IRotaService
    {
        public Task<ListResponse<RotaDto>> GetRotasAsync(GetRotasRequest requestParameters, CancellationToken cancellationToken);

        public Task<RotaDto> CreateRotaAsync(RotaDto requestBody, CancellationToken cancellationToken);

        public Task<RotaDto> UpdateRotaAsync(Guid rotaIdGuid, RotaDto requestBody, CancellationToken cancellationToken);

        public Task<Guid> DeleteRotaAsync(Guid rotaIdGuid, CancellationToken cancellationToken);
    }
}
