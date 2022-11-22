﻿using System;

namespace Timeboxed.Api.Models.DTOs
{
    public class RoleDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? ApplicationId { get; set; }

        public int[] Permissions { get; set; }
    }
}
