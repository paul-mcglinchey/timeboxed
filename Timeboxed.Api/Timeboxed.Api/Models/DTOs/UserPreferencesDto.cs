using System;

namespace Timeboxed.Api.Models.DTOs
{
    public class UserPreferencesDto
    {
        public Guid? Id { get; set; }

        public Guid? DefaultGroup { get; set; }
    }
}
