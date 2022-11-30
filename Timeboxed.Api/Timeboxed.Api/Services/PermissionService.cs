using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Models.Responses.Common;
using Timeboxed.Api.Services.Interfaces;
using Timeboxed.Data;

namespace Timeboxed.Api.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly TimeboxedContext context;

        public PermissionService(TimeboxedContext context)
        {
            this.context = context;
        }

        public async Task<ListResponse<PermissionResponse>> GetPermissionsAsync(CancellationToken cancellationToken)
        {
            var permissions = await this.context.Permissions.Include(p => p.Applications).Select(p => new PermissionResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Language = p.Language,
                Applications = p.Applications.Select(a => a.Id).ToArray(),
            }).ToListAsync(cancellationToken);

            return new ListResponse<PermissionResponse>
            {
                Items = permissions,
                Count = permissions.Count
            };
        }
    }
}
