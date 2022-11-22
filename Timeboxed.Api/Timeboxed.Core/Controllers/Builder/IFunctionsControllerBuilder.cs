namespace Timeboxed.Core.Controllers.Builder;

using System;
using Microsoft.Extensions.Configuration;

public interface IFunctionsControllerBuilder
{
    IFunctionsControllerBuilder WithVersionController(Type startupType);

    IFunctionsControllerBuilder WithConfigurationController();

    IFunctionsControllerBuilder WithDatabaseController();

    IConfigurationBuilder Build();
}