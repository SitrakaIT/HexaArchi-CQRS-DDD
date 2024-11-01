using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SIT.Shared.Abstractions.Interfaces;

namespace SIT.Shared.Extensions;

public static class ServiceProviderExtensions
{
    public static TOptions GetOptions<TOptions>(this IServiceProvider serviceProvider)
        where TOptions : class, IAppOptions
    {
        return serviceProvider.GetService<IOptions<TOptions>>()?.Value;
    }
}