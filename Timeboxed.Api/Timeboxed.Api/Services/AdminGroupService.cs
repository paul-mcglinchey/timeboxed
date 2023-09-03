using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Models.Responses.Common;
using Timeboxed.Api.Services.Interfaces;
using Timeboxed.Api.Services.Projections;
using Timeboxed.Core.AccessControl.Interfaces;
using Timeboxed.Core.Exceptions;
using Timeboxed.Data;

namespace Timeboxed.Api.Services;

public class AdminGroupService : IAdminGroupService
{
    private readonly TimeboxedContext context;
    private readonly IGroupContextProvider groupContextProvider;
    private readonly IUserContextProvider userContextProvider;

    public AdminGroupService(TimeboxedContext context, IGroupContextProvider groupContextProvider, IUserContextProvider userContextProvider)
    {
        this.context = context;
        this.groupContextProvider = groupContextProvider;
        this.userContextProvider = userContextProvider;
    }

    public async Task<ListResponse<GroupResponse>> GetGroupsAsync(AdminGetGroupsRequest request, CancellationToken cancellationToken)
    {
        var query = this.context.Groups.AsQueryable();

        if (request.Name != null && request.Name.Trim() != string.Empty)
        {
            query = query.Where(g => g.Name.ToLower().Contains(request.Name.ToLower()));
        }

        return new ListResponse<GroupResponse>
        {
            Count = await query.CountAsync(cancellationToken),
            Items = await query.Select(Common.MapEFGroupToResponse).ToListAsync(cancellationToken),
        };
    }

    public async Task UpdateGroupAsync(AdminUpdateGroupRequest request, CancellationToken cancellationToken)
    {
        var groupId = this.groupContextProvider.GroupId;

        var group = await this.context.Groups
            .Where(g => g.Id == groupId)
            .Include(g => g.Applications)
            .SingleOrDefaultAsync(cancellationToken)
        ?? throw new EntityNotFoundException($"Group {groupId} not found");

        group.Applications = await this.context.Applications.Where(a => request.Applications.Contains(a.Id)).ToListAsync(cancellationToken);

        group.AddTracking(this.userContextProvider.UserId);

        await this.context.SaveChangesAsync(cancellationToken);
    }
}
