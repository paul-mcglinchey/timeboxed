using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Timeboxed.Api;
using Timeboxed.Api.Services;
using Timeboxed.Api.Services.Interfaces;
using Timeboxed.Core.AccessControl.Interfaces;
using Timeboxed.Core.AccessControl.Services;
using Timeboxed.Core.Converters;
using Timeboxed.Core.Extensions;
using Timeboxed.Data;
using Timeboxed.Data.Enums;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Timeboxed.Api
{
    public class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder) => builder.ConfigurationBuilder.ConfigureAppConfiguration().ConfigureDefaultApiControllers(typeof(Startup));

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<IUserAuthorisationService<TimeboxedPermission>, UserAuthorizationService>();

            builder.Services.AddAccessControl<TimeboxedPermission, UserAuthorizationService>();

            this.ConfigureServices(builder, builder.GetContext().Configuration);
            this.ConfigureNewtonsoft();
        }

        private void ConfigureServices(IFunctionsHostBuilder builder, IConfiguration configuration)
        {
            builder.Services.AddSingleton<JwtSecurityTokenHandler>();

            builder.Services.AddDbContext<TimeboxedContext>(options =>
                options.UseSqlServer(configuration["TimeboxedConnectionString"], options => options.EnableRetryOnFailure(3)));

            builder.Services.AddTransient<IUserValidator, UserValidator>();

            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IGroupService, GroupService>();
            builder.Services.AddTransient<IGroupUserService, GroupUserService>();
            builder.Services.AddTransient<IRoleService, RoleService>();
            builder.Services.AddTransient<IApplicationService, ApplicationService>();
            builder.Services.AddTransient<IPermissionService, PermissionService>();

            builder.Services.AddTransient<IClientService, ClientService>();
            builder.Services.AddTransient<ISessionService, SessionService>();

            builder.Services.AddTransient<IRotaService, RotaService>();
            builder.Services.AddTransient<IEmployeeService, EmployeeService>();
            builder.Services.AddTransient<IScheduleService, ScheduleService>();

            builder.Services.AddTransient<IGroupContextProvider, GroupContextProvider>();
            builder.Services.AddTransient<IUserContextProvider, UserContextProvider>();

            builder.Services.AddAutoMapper(typeof(Startup));
        }

        private void ConfigureNewtonsoft()
        {
            var resolver = new DefaultContractResolver();
            resolver.NamingStrategy = new CamelCaseNamingStrategy
            {
                ProcessDictionaryKeys = false,
            };

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = resolver,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Converters = new List<JsonConverter> { new NullableDateOnlyJsonConverter() }
            };
        }
    }
}
