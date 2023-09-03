using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Timeboxed.Core.FunctionWrappers
{
    public interface IHttpRequestWrapper<TPermission>
    {
        public Task<ActionResult> ExecuteAsync(
            List<TPermission> requiredPermissions,
            Func<Task<ActionResult>> implementation,
            CancellationToken cancellationToken = default,
            bool adminRequired = false,
            [CallerMemberName] string functionName = null);

        public Task<ActionResult<TResult>> ExecuteAsync<TResult>(
            List<TPermission> requiredPermissions,
            Func<Task<ActionResult<TResult>>> implementation,
            CancellationToken cancellationToken = default,
            bool adminRequired = false, 
            [CallerMemberName] string functionName = null);
    }
}
