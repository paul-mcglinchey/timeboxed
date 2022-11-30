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
    public class ApplicationService : IApplicationService
    {
        private readonly TimeboxedContext context;

        public ApplicationService(TimeboxedContext context)
        {
            this.context = context;
        }

        public async Task<ListResponse<ApplicationListResponse>> GetApplicationsAsync(CancellationToken cancellationToken)
        {
            var applications = await this.context.Applications
                .Include(a => a.Permissions)
                .Select(a => new ApplicationListResponse 
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    BackgroundImage = a.BackgroundImage,
                    BackgroundVideo = a.BackgroundVideo,
                    Colour = a.Colour,
                    Url = a.Url,
                    Icon = a.Icon,
                })
                .ToListAsync(cancellationToken);

            return new ListResponse<ApplicationListResponse>
            {
                Items = applications,
                Count = applications.Count,
            };
        }
    }
}
