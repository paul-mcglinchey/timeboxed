using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Models.Responses.Common;

namespace Timeboxed.Api.Services.Interfaces
{
    public interface IApplicationService
    {
        public Task<ListResponse<ApplicationListResponse>> GetApplicationsAsync(CancellationToken cancellationToken);
    }
}
