using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIT.Shared.Abstractions;

namespace SIT.Infrastructure.Extensions;

public static class EntityTypeBuilderExtensions
{
    public static void ConfigureBaseEntity<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : BaseEntity
    {
        builder.HasKey(entity => entity.Id);
        
        builder.Property(p => p.CreationUser)
            .IsRequired()
            .HasDefaultValue("SIT")
            .HasMaxLength(200);
        
        builder.Property(p => p.CreationDate)
            .IsRequired()
            .HasDefaultValue(DateTime.Now)
            .HasColumnType("DATE");
        
        builder.Property(p => p.EditionUser).HasMaxLength(200);
        
        builder.Property(p => p.EditionDate).HasColumnType("DATE");
    }
}