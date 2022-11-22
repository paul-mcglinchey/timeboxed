using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace Timeboxed.Core.FunctionWrappers
{
    public class FunctionWrapper : IFunctionWrapper
    {
        private readonly ILogger<FunctionWrapper> logger;

        public FunctionWrapper(ILogger<FunctionWrapper> logger)
        {
            this.logger = logger;
        }

        public async Task ExecuteAsync(Func<Task> implementation, CancellationToken cancellationToken, [CallerMemberName] string functionName = null) =>
            await this.ExecuteAsync<object>(
                async () =>
                {
                    await implementation();
                    return default;
                },
                cancellationToken,
                functionName);

        public async Task<T> ExecuteAsync<T>(Func<Task<T>> implementation, CancellationToken cancellationToken, [CallerMemberName] string functionName = null)
        {
            try
            {
                this.logger.LogInformation($"Entered {functionName}.");
                return await implementation();
            }
            catch (Exception e)
            {
                this.logger.LogError(e, $"Error in {functionName}.");
                throw;
            }
            finally
            {
                this.logger.LogInformation($"Leaving {functionName}.");
            }
        }
    }
}
