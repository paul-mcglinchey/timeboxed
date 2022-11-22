using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Timeboxed.Core.AccessControl.Interfaces;
using Timeboxed.Core.Exceptions;
using Timeboxed.Data;

namespace Timeboxed.Core.AccessControl.Services
{
    internal class GroupValidator : IGroupValidator
    {
        private readonly TimeboxedContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        private Guid? groupId;

        public GroupValidator(TimeboxedContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        public Guid GroupId => this.groupId ?? throw new NotSupportedException("Group ID cannot be used before validation");

        public async Task Validate(CancellationToken cancellationToken = default)
        {
            var req = this.httpContextAccessor.HttpContext?.Request;

            this.Validate(req, cancellationToken);
        }

        public async Task Validate(HttpRequest req, CancellationToken cancellation = default)
        {
            if (!req.Query.TryGetValue("groupId", out var groupId))
            {
                throw new BadRequestException("Group ID not supplied in query parameters.");
            }

            await Validate(groupId, cancellation);
        }

        public async Task Validate(string groupId, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(groupId, out var groupIdGuid))
            {
                throw new BadRequestException("Group ID supplied is not a valid GUID.");
            }

            var group = await this.context.Groups.Where(g => g.Id.Equals(groupIdGuid)).SingleOrDefaultAsync(cancellationToken);

            if (group == null)
            {
                throw new EntityNotFoundException("Group not found");
            }

            this.groupId = groupIdGuid;
        }
    }
}
