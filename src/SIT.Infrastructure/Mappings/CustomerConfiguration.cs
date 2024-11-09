using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIT.Core.Domain.Entities;
using SIT.Infrastructure.Extensions;

namespace SIT.Infrastructure.Mappings;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customer");
        builder.ConfigureBaseEntity();
        
        builder.Property(p => p.Name).HasMaxLength(200).IsRequired();
        builder.Property(p => p.CustomerType).IsRequired();
    }
}