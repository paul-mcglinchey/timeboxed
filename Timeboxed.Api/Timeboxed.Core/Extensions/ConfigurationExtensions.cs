using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Timeboxed.Core.Controllers.Builder;
using Timeboxed.Core.Extensions.Options;

namespace Timeboxed.Core.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IConfigurationBuilder ConfigureAppConfiguration(this IConfigurationBuilder builder)
        {
            var environmentName = Environment.GetEnvironmentVariable("EnvironmentName");

            return builder.ConfigureAppConfiguration(
                new AppConfigOptions
                {
                    ConnectionString = Environment.GetEnvironmentVariable("AppConfigConnectionString"),
                    EnvironmentName = environmentName != null ? environmentName : "local",
                });
        }

        public static IConfigurationBuilder ConfigureAppConfiguration(this IConfigurationBuilder builder, Action<AppConfigOptions> action)
        {
            var appConfigOptions = new AppConfigOptions();
            action.Invoke(appConfigOptions);
            return builder.ConfigureAppConfiguration(appConfigOptions);
        }

        public static IConfigurationBuilder ConfigureDefaultApiControllers(this IConfigurationBuilder builder, Type startupType) =>
            builder.ConfigureControllers()
                .WithVersionController(startupType)
                .WithConfigurationController()
                .Build();

        public static IFunctionsControllerBuilder ConfigureControllers(this IConfigurationBuilder builder) =>
            new FunctionsControllerBuilder(builder);

        public static IConfigurationBuilder ConfigureAppConfiguration(this IConfigurationBuilder builder, AppConfigOptions appConfigOptions)
        {
            return builder.AddAzureAppConfiguration(
                options =>
                {
                    options.Connect(appConfigOptions.ConnectionString);

                    options.Select("*");
                    options.Select("*", appConfigOptions.EnvironmentName);

                    options.UseFeatureFlags(featureFlagOptions => featureFlagOptions.Label = appConfigOptions.EnvironmentName);

                    options.ConfigureKeyVault(kv =>
                    {
                        kv.SetCredential(new DefaultAzureCredential());
                    });
                },
                true);
        }
    }
}
