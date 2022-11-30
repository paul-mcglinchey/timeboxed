using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timeboxed.Api.Models;
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
        private readonly IMapper mapper;
        private readonly IUserContextProvider userContextProvider;

        public UserController(
            ILogger<UserController> logger,
            IFunctionWrapper functionWrapper,
            IHttpRequestWrapper<TimeboxedPermission> httpRequestWrapper,
            TimeboxedContext context,
            IUserService userService,
            IMapper mapper,
            IUserContextProvider userContextProvider)
        {
            this.logger = logger;
            this.functionWrapper = functionWrapper;
            this.httpRequestWrapper = httpRequestWrapper;
            this.context = context;
            this.userService = userService;
            this.mapper = mapper;
            this.userContextProvider = userContextProvider;
        }

        [FunctionName("Signup")]
        public async Task<ActionResult<string>> Signup(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "users/signup")] HttpRequest req,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.functionWrapper.ExecuteAsync<ActionResult>(
                async () =>
                {
                    var userRequest = await req.ConstructRequestModelAsync<UserRequest>();

                    if (userRequest.Username == null || userRequest.Email == null || userRequest.Password == null)
                    {
                        return new BadRequestObjectResult(new { message = "Fields missing from request." });
                    }

                    if (await this.userService.UserExistsAsync(userRequest, cancellationToken))
                    {
                        return new BadRequestObjectResult(new { message = "User already exists." });
                    }

                    var user = await this.userService.CreateUserAsync(userRequest, cancellationToken);

                    return new OkObjectResult(user);
                },
                cancellationToken);

        [FunctionName("Login")]
        public async Task<ActionResult<string>> Login(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "users/login")] HttpRequest req,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.functionWrapper.ExecuteAsync<ActionResult>(
                async () =>
                {
                    var userRequest = await req.ConstructRequestModelAsync<UserRequest>();

                    if (userRequest?.UsernameOrEmail == null || userRequest?.Password == null)
                    {
                        return new BadRequestObjectResult("Fields missing from request.");
                    }

                    if (!await this.userService.UserExistsAsync(userRequest, cancellationToken))
                    {
                        return new BadRequestObjectResult("Incorrect email or username.");
                    }

                    var user = await this.userService.AuthenticateUserAsync(userRequest, cancellationToken);

                    return user != null
                        ? new OkObjectResult(user)
                        : new UnauthorizedResult();
                },
                cancellationToken);

        [FunctionName("AuthenticateUser")]
        public async Task<ActionResult> Authenticate(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users/authenticate")] HttpRequest req,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.httpRequestWrapper.ExecuteAsync(
                new List<TimeboxedPermission> { TimeboxedPermission.ApplicationAccess },
                async () =>
                {
                    return new OkObjectResult(await this.userService.GetUserByIdAsync(this.userContextProvider.UserId, cancellationToken));
                },
                cancellationToken);

        [FunctionName("UpdateUserPreferences")]
        public async Task<ActionResult> UpdateUserPreferences(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "users/preferences")] HttpRequest req,
            ILogger logger,
            CancellationToken cancellationToken) =>
            await this.httpRequestWrapper.ExecuteAsync(
                new List<TimeboxedPermission> { TimeboxedPermission.ApplicationAccess },
                async () =>
                {
                    var requestBody = await req.ConstructRequestModelAsync<UserPreferencesResponse>();

                    return new OkObjectResult(await this.userService.UpdateUserPreferencesAsync(requestBody, cancellationToken));
                },
                cancellationToken);
    }
}
