namespace Timeboxed.Core.Controllers.Builder
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Microsoft.Extensions.Configuration;

    internal class FunctionsControllerBuilder : IFunctionsControllerBuilder
    {
        internal const string DisableVersionController = nameof(DisableVersionController);
        internal const string DisableConfigurationController = nameof(DisableConfigurationController);
        internal const string DisableDatabaseController = nameof(DisableDatabaseController);
        internal const string ApiAssemblyName = nameof(ApiAssemblyName);

        private readonly IConfigurationBuilder builder;
        private readonly IDictionary<string, string> controllerSettings;

        internal FunctionsControllerBuilder(IConfigurationBuilder builder)
        {
            this.builder = builder;
            this.controllerSettings = new Dictionary<string, string>
            {
                { DisableVersionController, true.ToString() },
                { DisableConfigurationController, true.ToString() },
                { DisableDatabaseController, true.ToString() },
            };
        }

        public IFunctionsControllerBuilder WithVersionController(Type startupType)
        {
            this.controllerSettings[DisableVersionController] = false.ToString();
            this.controllerSettings[ApiAssemblyName] = Assembly.GetAssembly(startupType).FullName;
            return this;
        }

        public IFunctionsControllerBuilder WithConfigurationController()
        {
            this.controllerSettings[DisableConfigurationController] = false.ToString();
            return this;
        }

        public IFunctionsControllerBuilder WithDatabaseController()
        {
            this.controllerSettings[DisableDatabaseController] = false.ToString();
            return this;
        }

        public IConfigurationBuilder Build() => this.builder.AddInMemoryCollection(this.controllerSettings);
    }
}
