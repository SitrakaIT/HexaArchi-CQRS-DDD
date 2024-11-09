using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SIT.Infrastructure.Contexts;
using SIT.Shared.AppSettings;
using SIT.Shared.Extensions;

namespace SIT.API.Extensions;

[ExcludeFromDescription]
public static class ServicesCollectionExtensions
{
    private const int DbMaxRetryCount = 3;
    private const int DbCommandTimeout = 35;
    private const string DbMigrationAssemblyName = "SIT.API";
    
    public static IServiceCollection AddWriteDbContext(this IServiceCollection services)
    {
        services.AddDbContext<WriteDbContext>((serviceProvider, optionsBuilder) => 
            ConfigureDbContext<WriteDbContext>(serviceProvider, optionsBuilder, QueryTrackingBehavior.TrackAll)
        );

        return services;
    }

    private static void ConfigureDbContext<TContext>(
        IServiceProvider serviceProvider,
        DbContextOptionsBuilder optionsBuilder,
        QueryTrackingBehavior queryTrackingBehavior) where TContext : DbContext
    {
        var logger = serviceProvider.GetRequiredService<ILogger<TContext>>();
        var options = serviceProvider.GetOptions<ConnectionOptions>();
        var environment = serviceProvider.GetRequiredService<IHostEnvironment>();
        var isDevelopment = environment.IsDevelopment();

        optionsBuilder
            .UseSqlServer(options.SqlServerConnection, sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions
                    .MigrationsAssembly(DbMigrationAssemblyName)
                    .EnableRetryOnFailure(DbMaxRetryCount)
                    .CommandTimeout(DbCommandTimeout);
            })
            .EnableDetailedErrors(isDevelopment)
            .EnableSensitiveDataLogging(isDevelopment)
            .UseQueryTrackingBehavior(queryTrackingBehavior)
            .LogTo((eventId, _) => eventId.Id == CoreEventId.ExecutionStrategyRetrying, eventData =>
            {
                if (eventData is not ExecutionStrategyEventData executionStrategyEventData)
                    return;

                var exceptions = executionStrategyEventData.ExceptionsEncountered;

                logger.LogWarning(
                    "----- DbContext: Retry #{Count} with delay {Delay} due to error: {Message}",
                    exceptions.Count,
                    executionStrategyEventData.Delay,
                    exceptions[^1].Message);
            });
    }
}