using System.Collections.Generic;
using Timeboxed.Api.Models.DTOs;

namespace Timeboxed.Api.Models
{
    public class MetaInfo
    {
        public ICollection<ApplicationDto> Applications { get; set; }

        public ICollection<RoleDto> Roles { get; set; }

        public ICollection<PermissionDto> Permissions { get; set; }
    }
}
