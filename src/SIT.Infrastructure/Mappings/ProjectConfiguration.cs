using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIT.Core.Domain.Aggregates;
using SIT.Core.Domain.Entities;
using SIT.Infrastructure.Extensions;

namespace SIT.Infrastructure.Mappings;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ConfigureBaseEntity();
        
        builder.Property(p => p.Title).IsRequired().HasMaxLength(200);
        builder.Property(p => p.CustomerId).IsRequired();
        
        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(p => p.CustomerId);

        builder.OwnsOne(p => p.Location, location =>
        {
            location.Property(loc => loc.ZipCode).HasMaxLength(10).IsRequired();
            location.Property(loc => loc.City).HasMaxLength(100).IsRequired();
            location.Property(loc => loc.Region).HasMaxLength(100).IsRequired();
            location.Property(loc => loc.Country).HasMaxLength(100).IsRequired();
        });
    }
}