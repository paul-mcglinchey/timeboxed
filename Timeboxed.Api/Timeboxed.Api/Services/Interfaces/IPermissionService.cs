using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models;
using Timeboxed.Api.Models.DTOs;

namespace Timeboxed.Api.Services.Interfaces
{
    public interface IPermissionService
    {
        public Task<ListResponse<PermissionDto>> GetPermissionsAsync(CancellationToken cancellationToken);
    }
}
