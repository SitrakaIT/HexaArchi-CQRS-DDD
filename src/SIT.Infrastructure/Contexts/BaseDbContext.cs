using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SIT.Infrastructure.Extensions;

namespace SIT.Infrastructure.Contexts;

public abstract class BaseDbContext<TContext>(DbContextOptions<TContext> options) : DbContext(options)
    where TContext : DbContext
{
    private const string DbCollation = "Latin1_General_CI_AI";

    public override ChangeTracker ChangeTracker
    {
        get
        {
            base.ChangeTracker.LazyLoadingEnabled = false;
            base.ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
            base.ChangeTracker.DeleteOrphansTiming = CascadeTiming.OnSaveChanges;

            return base.ChangeTracker;
        }
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<string>()
            .AreUnicode(false)
            .HaveMaxLength(255);
        
        base.ConfigureConventions(configurationBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation(DbCollation)
            .RemoveCascadeDeleteConvention();
        
        base.OnModelCreating(modelBuilder);
    }
}