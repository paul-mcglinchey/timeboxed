using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models;
using Timeboxed.Api.Models.DTOs;

namespace Timeboxed.Api.Services.Interfaces
{
    public interface IApplicationService
    {
        public Task<ListResponse<ApplicationDto>> GetApplicationsAsync(CancellationToken cancellationToken);
    }
}
