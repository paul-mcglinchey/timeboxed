using System.Runtime.CompilerServices;

namespace Timeboxed.Core.FunctionWrappers
{
    public interface IFunctionWrapper
    {
        public Task ExecuteAsync(Func<Task> implementation, CancellationToken cancellationToken, [CallerMemberName] string functionName = null);

        public Task<T> ExecuteAsync<T>(Func<Task<T>> implementation, CancellationToken cancellationToken, [CallerMemberName] string functionName = null);
    }
}
