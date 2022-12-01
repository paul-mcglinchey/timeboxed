using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models.Requests;
using Timeboxed.Api.Models.Responses;
using Timeboxed.Api.Services.Interfaces;
using Timeboxed.Core.AccessControl.Interfaces;
using Timeboxed.Core.Extensions;
using Timeboxed.Core.FunctionWrappers;
using Timeboxed.Data;
using Timeboxed.Data.Enums;

namespace Timeboxed.Api.Controllers
{
    public class UserController
    {
        private readonly ILogger<UserController> logger;
        private readonly IFunctionWrapper functionWrapper;
        private readonly IHttpRequestWrapper<TimeboxedPermission> httpRequestWrapper;
        private readonly TimeboxedContext context;
        private readonly IUserService userService;
        private readonly IUserContextProvider userContextProvider;

        public UserController(
            ILogger<UserController> logger,
            IFunctionWrapper functionWrapper,
            IHttpRequestWrapper<TimeboxedPermission> httpRequestWrapper,
            TimeboxedContext context,
            IUserService userService,
            IUserContextProvider userContextProvider)
        {
            this.logger = logger;
            this.functionWrapper = functionWrapper;
            this.httpRequestWrapper = httpRequestWrapper;
            this.context = context;
            this.userService = userService;
            this.userContextProvider = userContextProvider;
        }

        [FunctionName("Signup")]
        public async Task<ActionResult> Signup(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "users/signup")] HttpRequest req,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.functionWrapper.ExecuteAsync(
                async () =>
                {
                    var request = await req.ConstructRequestModelAsync<SignupRequest>();

                    if (request.Username == null || request.Email == null || request.Password == null)
                    {
                        return new BadRequestObjectResult(new { message = "Fields missing from request." });
                    }

                    if (await this.userService.UserExistsAsync(request.Email, request.Username, cancellationToken))
                    {
                        return new BadRequestObjectResult(new { message = "Username or email is already in use." });
                    }

                    return new OkObjectResult(await this.userService.SignupAsync(request, cancellationToken));
                },
                cancellationToken);

        [FunctionName("Login")]
        public async Task<ActionResult> Login(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "users/login")] HttpRequest req,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.functionWrapper.ExecuteAsync(
                async () =>
                {
                    var request = await req.ConstructRequestModelAsync<LoginRequest>();

                    if (request?.UsernameOrEmail == null || request?.Password == null)
                    {
                        return new BadRequestObjectResult("Fields missing from request.");
                    }

                    if (!await this.userService.UserExistsAsync(request.UsernameOrEmail, cancellationToken))
                    {
                        return new BadRequestObjectResult("Incorrect email or username.");
                    }

                    var user = await this.userService.LoginAsync(request, cancellationToken);

                    return user != null
                        ? new OkObjectResult(user)
                        : new UnauthorizedResult();
                },
                cancellationToken);

        [FunctionName("AuthenticateUser")]
        public async Task<ActionResult<UserResponse>> Authenticate(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users/authenticate")] HttpRequest req,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.httpRequestWrapper.ExecuteAsync<UserResponse>(
                new List<TimeboxedPermission> { TimeboxedPermission.ApplicationAccess },
                async () =>
                {
                    return new OkObjectResult(await this.userService.GetUserByIdAsync(this.userContextProvider.UserId, cancellationToken));
                },
                cancellationToken);

        [FunctionName("UpdateUserPreferences")]
        public async Task<ActionResult<UserPreferencesResponse>> UpdateUserPreferences(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "users/preferences")] HttpRequest req,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.httpRequestWrapper.ExecuteAsync<UserPreferencesResponse>(
                new List<TimeboxedPermission> { TimeboxedPermission.ApplicationAccess },
                async () =>
                {
                    var request = await req.ConstructRequestModelAsync<UpdateUserPreferencesRequest>();

                    return new OkObjectResult(await this.userService.UpdateUserPreferencesAsync(request, cancellationToken));
                },
                cancellationToken);
    }
}
