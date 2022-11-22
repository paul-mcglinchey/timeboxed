﻿using System.Collections.Generic;

namespace Timeboxed.Api.Models.Requests
{
    public class AddGroupRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Colour { get; set; }

        public List<int> Applications { get; set; }
    }
}
