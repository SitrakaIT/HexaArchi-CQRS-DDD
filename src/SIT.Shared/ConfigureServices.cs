using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using SIT.Shared.Abstractions.Interfaces;
using SIT.Shared.AppSettings;

namespace SIT.Shared;

[ExcludeFromCodeCoverage]
public static class ConfigureServices
{
    public static IServiceCollection AddAppSettings(this IServiceCollection services)
    {
        return services.AddOptionsWithValidations<ConnectionOptions>();
    }

    private static IServiceCollection AddOptionsWithValidations<TOptions>(this IServiceCollection services)
        where TOptions : class, IAppOptions
    {
        return services
            .AddOptions<TOptions>()
            .BindConfiguration(TOptions.ConfigurationSectionPath, option => option.BindNonPublicProperties = true)
            .ValidateDataAnnotations()
            .ValidateOnStart()
            .Services;
    }
}