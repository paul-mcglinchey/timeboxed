using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Models.Responses.Common;
using Timeboxed.Api.Services.Interfaces;
using Timeboxed.Api.Services.Projections;
using Timeboxed.Data;

namespace Timeboxed.Api.Services;

public class AdminUserService : IAdminUserService
{
    private readonly TimeboxedContext context;

    public AdminUserService(TimeboxedContext context)
    {
        this.context = context;
    }

    public async Task<ListResponse<UserResponse>> GetUsersAsync(AdminGetUsersRequest request, CancellationToken cancellationToken)
    {
        var usersQuery = this.context.Users
            .Include(u => u.Groups)
                .ThenInclude(g => g.Roles)
            .AsQueryable();

        if (request?.GroupId != null)
        {
            usersQuery = usersQuery.Where(u => u.Groups.Any(g => g.GroupId == request.GroupId));
        }

        if (request?.Username != null && request?.Username.Trim() != string.Empty)
        {
            usersQuery = usersQuery.Where(u => u.Username.ToLower().Contains(request.Username.Trim().ToLower()));
        }

        if (request?.Email != null && request?.Email.Trim() != string.Empty)
        {
            usersQuery = usersQuery.Where(u => u.Email.ToLower().Contains(request.Email.Trim().ToLower()));
        }

        return new ListResponse<UserResponse>
        {
            Count = await usersQuery.CountAsync(cancellationToken),
            Items = await usersQuery.Select(Common.MapEFUserToResponse).ToListAsync(cancellationToken),
        };
    }
}
