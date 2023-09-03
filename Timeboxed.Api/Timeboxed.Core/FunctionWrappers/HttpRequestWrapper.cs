using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web.Http;
using Timeboxed.Core.AccessControl.Interfaces;
using Timeboxed.Core.Exceptions;

namespace Timeboxed.Core.FunctionWrappers
{
    public sealed class HttpRequestWrapper<TPermission> : IHttpRequestWrapper<TPermission>
    {
        private readonly ILogger<HttpRequestWrapper<TPermission>> logger;
        private readonly IAuthenticator authenticator;
        private readonly IGroupValidator groupValidator;
        private readonly IUserAuthorisationService<TPermission> userAuthorizationService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public HttpRequestWrapper(
            ILogger<HttpRequestWrapper<TPermission>> logger,
            IAuthenticator authenticator,
            IGroupValidator groupValidator,
            IUserAuthorisationService<TPermission> userAuthorizationService,
            IHttpContextAccessor httpContextAccessor)
        {
            this.logger = logger;
            this.authenticator = authenticator;
            this.groupValidator = groupValidator;
            this.userAuthorizationService = userAuthorizationService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ActionResult> ExecuteAsync(
            List<TPermission> requiredPermissions,
            Func<Task<ActionResult>> implementation,
            CancellationToken cancellationToken,
            bool adminRequired = false,
            [CallerMemberName] string functionName = null) => (await this.ExecuteAsync<object>(requiredPermissions, async () => await implementation(), cancellationToken, adminRequired, functionName)).Result;

        public async Task<ActionResult<TResult>> ExecuteAsync<TResult>(
            List<TPermission> requiredPermissions,
            Func<Task<ActionResult<TResult>>> implementation,
            CancellationToken cancellationToken,
            bool adminRequired = false,
            [CallerMemberName] string functionName = null)
        {
            try
            {
                // Authentication
                if (!await this.authenticator.AuthenticateAsync())
                {
                    this.logger.LogDebug("401: Unable to authenticate");
                    return new StatusCodeResult((int)HttpStatusCode.Unauthorized);
                }
            }
            catch (SecurityTokenSignatureKeyNotFoundException ex)
            {
                this.logger.LogError(ex, $"Unable to find JWT signature to authenticate in {functionName}.");
                return new StatusCodeResult((int)HttpStatusCode.Unauthorized);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Unexpected exception in user authentication for {functionName}.");
                return new StatusCodeResult(500);
            }

            try
            {
                if (!await this.userAuthorizationService.IsAuthorised(requiredPermissions, adminRequired))
                {
                    this.logger.LogDebug("401: Unable to authorize");
                    return new StatusCodeResult((int)HttpStatusCode.Unauthorized);
                }

                this.logger.LogInformation($"Entered {functionName}.");

                return await implementation();
            }
            catch (BadRequestException ex)
            {
                return new BadRequestObjectResult(new { Id = Guid.NewGuid(), Message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                return new NotFoundObjectResult(new { Id = Guid.NewGuid(), Message = ex.Message });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error in {functionName}.");
                return new InternalServerErrorResult();
            }
            finally
            {
                this.logger.LogInformation($"Leaving {functionName}.");
            };
        }
    }
}
