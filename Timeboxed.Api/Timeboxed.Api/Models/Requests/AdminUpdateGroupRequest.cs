﻿using System;
using System.Collections.Generic;

namespace Timeboxed.Api.Models.Requests;

public class AdminUpdateGroupRequest
{
    public List<int> Applications { get; set; }

    public List<Guid> Users { get; set; }
}
