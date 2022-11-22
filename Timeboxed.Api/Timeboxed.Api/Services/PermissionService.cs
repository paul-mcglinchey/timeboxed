using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models;
using Timeboxed.Api.Models.DTOs;
using Timeboxed.Api.Services.Interfaces;
using Timeboxed.Data;

namespace Timeboxed.Api.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IMapper mapper;
        private readonly TimeboxedContext context;

        public PermissionService(IMapper mapper, TimeboxedContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<ListResponse<PermissionDto>> GetPermissionsAsync(CancellationToken cancellationToken)
        {
            var permissions = await this.context.Permissions.Include(p => p.Applications).Select(p => new PermissionDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Language = p.Language,
                Applications = p.Applications.Select(a => a.Id).ToArray(),
            }).ToListAsync(cancellationToken);

            return new ListResponse<PermissionDto>
            {
                Items = permissions,
                Count = permissions.Count
            };
        }
    }
}
