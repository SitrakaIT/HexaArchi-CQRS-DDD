using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using SIT.Core.Domain.Repositories.Customers;
using SIT.Infrastructure.Contexts;
using SIT.Infrastructure.Repositories;
using SIT.Infrastructure.Repositories.Commands;
using SIT.Shared.Abstractions.Interfaces;

namespace SIT.Infrastructure;

[ExcludeFromCodeCoverage]
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
            .AddScoped<WriteDbContext>()
            .AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static IServiceCollection AddWriteOnlyRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<ICustomerWriteOnlyRepository, CustomerWriteOnlyRepository>();
    }
}