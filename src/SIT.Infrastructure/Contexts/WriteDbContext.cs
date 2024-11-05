using Microsoft.EntityFrameworkCore;
using SIT.Core.Domain.Aggregates;
using SIT.Core.Domain.Entities;
using SIT.Infrastructure.Mappings;

namespace SIT.Infrastructure.Contexts;

public class WriteDbContext(DbContextOptions<WriteDbContext> options) : BaseDbContext<WriteDbContext>(options)
{
    public DbSet<Customer> Customers => Set<Customer>();
    
    public DbSet<Project> Projects => Set<Project>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .ApplyConfiguration(new CustomerConfiguration())
            .ApplyConfiguration(new ProjectConfiguration());
    }
}