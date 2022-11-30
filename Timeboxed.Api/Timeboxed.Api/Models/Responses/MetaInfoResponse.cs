using System.Collections.Generic;

namespace Timeboxed.Api.Models.Responses
{
    public class MetaInfoResponse
    {
        public ICollection<ApplicationListResponse> Applications { get; set; }

        public ICollection<RoleResponse> Roles { get; set; }

        public ICollection<PermissionResponse> Permissions { get; set; }
    }
}
