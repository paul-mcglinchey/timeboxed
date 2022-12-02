using Microsoft.Extensions.DependencyInjection;
using Timeboxed.Core.AccessControl.Interfaces;
using Timeboxed.Core.AccessControl.Services;
using Timeboxed.Core.FunctionWrappers;

namespace Timeboxed.Core.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddFunctionWrapper(this IServiceCollection services) =>
            services.AddScoped<IFunctionWrapper, FunctionWrapper>()
                    .AddHttpContextAccessor();

        public static IServiceCollection AddAccessControl(this IServiceCollection services) =>
            services.AddAccessControl<int, UserAuthorizationService>();

        public static IServiceCollection AddAccessControl<TPermission, TUserAuthorizationService>(this IServiceCollection services)
            where TUserAuthorizationService : class, IUserAuthorisationService<TPermission>
        {
            return services
                    .AddScoped<IAuthenticator, HttpAuthenticator>()
                    .AddScoped<IHttpRequestWrapper<TPermission>, HttpRequestWrapper<TPermission>>()
                    .AddScoped<IGroupValidator, GroupValidator>()
                    .AddScoped<IUserAuthorisationService<TPermission>, TUserAuthorizationService>()
                    .AddFunctionWrapper();
        }
    }
}
