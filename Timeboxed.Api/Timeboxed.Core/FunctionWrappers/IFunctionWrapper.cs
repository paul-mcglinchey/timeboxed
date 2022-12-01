using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Timeboxed.Core.FunctionWrappers
{
    public interface IFunctionWrapper
    {
        public Task<ActionResult> ExecuteAsync(Func<Task<ActionResult>> implementation, CancellationToken cancellationToken, [CallerMemberName] string functionName = null);

        public Task<ActionResult<T>> ExecuteAsync<T>(Func<Task<ActionResult<T>>> implementation, CancellationToken cancellationToken, [CallerMemberName] string functionName = null);
    }
}
