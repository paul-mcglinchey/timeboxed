using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models;
using Timeboxed.Api.Models.DTOs;
using Timeboxed.Api.Services.Interfaces;
using Timeboxed.Data;

namespace Timeboxed.Api.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IMapper mapper;
        private readonly TimeboxedContext context;

        public ApplicationService(IMapper mapper, TimeboxedContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<ListResponse<ApplicationDto>> GetApplicationsAsync(CancellationToken cancellationToken)
        {
            var applications = await this.context.Applications.Include(a => a.Permissions).ToListAsync(cancellationToken);

            return new ListResponse<ApplicationDto>
            {
                Items = this.mapper.Map<List<ApplicationDto>>(applications),
                Count = applications.Count,
            };
        }
    }
}
