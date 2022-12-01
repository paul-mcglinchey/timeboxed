using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using Timeboxed.Core.Exceptions;

namespace Timeboxed.Core.FunctionWrappers
{
    public class FunctionWrapper : IFunctionWrapper
    {
        private readonly ILogger<FunctionWrapper> logger;

        public FunctionWrapper(ILogger<FunctionWrapper> logger)
        {
            this.logger = logger;
        }

        public async Task<ActionResult> ExecuteAsync(Func<Task<ActionResult>> implementation, CancellationToken cancellationToken, [CallerMemberName] string functionName = null) =>
            (await this.ExecuteAsync<object>(async () => await implementation(), cancellationToken, functionName)).Result;

        public async Task<ActionResult<T>> ExecuteAsync<T>(Func<Task<ActionResult<T>>> implementation, CancellationToken cancellationToken, [CallerMemberName] string functionName = null)
        {
            try
            {
                this.logger.LogInformation($"Entered {functionName}.");
                return await implementation();
            }
            catch (BadRequestException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return new NotFoundObjectResult(ex.Message);
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
