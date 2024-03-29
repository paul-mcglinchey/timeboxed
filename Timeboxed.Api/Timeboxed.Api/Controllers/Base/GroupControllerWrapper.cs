﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Core.AccessControl.Interfaces;
using Timeboxed.Core.FunctionWrappers;
using Timeboxed.Data.Constants;

namespace Timeboxed.Api.Controllers.Base
{
    public abstract class GroupControllerWrapper<TController>
    {
        protected readonly ILogger<TController> logger;
        protected readonly IHttpRequestWrapper<int> httpRequestWrapper;
        private readonly IGroupValidator groupValidator;

        public GroupControllerWrapper(ILogger<TController> logger, IHttpRequestWrapper<int> httpRequestWrapper, IGroupValidator groupValidator)
        {
            this.logger = logger;
            this.httpRequestWrapper = httpRequestWrapper;
            this.groupValidator = groupValidator;
        }

        public async Task<ActionResult> ExecuteAsync(string groupId, Func<Task<ActionResult>> implementation, CancellationToken cancellationToken = default, bool adminRequired = false, [CallerMemberName] string functionName = null) =>
            await this.ExecuteAsync(new List<int> { TimeboxedPermissions.ApplicationAccess }, groupId, implementation, cancellationToken, adminRequired, functionName);

        public async Task<ActionResult> ExecuteAsync(List<int> permissions, string groupId, Func<Task<ActionResult>> implementation, CancellationToken cancellationToken = default, bool adminRequired = false, [CallerMemberName] string functionName = null)
        {
            await this.groupValidator.Validate(groupId, cancellationToken);

            return await this.httpRequestWrapper.ExecuteAsync(
                permissions,
                async () => await implementation(),
                cancellationToken,
                adminRequired,
                functionName);
        }

        public async Task<ActionResult<TResult>> ExecuteAsync<TResult>(string groupId, Func<Task<ActionResult<TResult>>> implementation, CancellationToken cancellationToken, bool adminRequired = false, [CallerMemberName] string functionName = null) =>
            await this.ExecuteAsync(new List<int> { TimeboxedPermissions.ApplicationAccess }, groupId, implementation, cancellationToken, adminRequired, functionName);

        public async Task<ActionResult<TResult>> ExecuteAsync<TResult>(List<int> permissions, string groupId, Func<Task<ActionResult<TResult>>> implementation, CancellationToken cancellationToken = default, bool adminRequired = false, [CallerMemberName] string functionName = null)
        {
            await this.groupValidator.Validate(groupId, cancellationToken);

            return await this.httpRequestWrapper.ExecuteAsync<TResult>(
                permissions,
                async () => await implementation(),
                cancellationToken,
                adminRequired,
                functionName);
        }
    }
}
