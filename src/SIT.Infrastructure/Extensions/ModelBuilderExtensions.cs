using Microsoft.EntityFrameworkCore;

namespace SIT.Infrastructure.Extensions;

public static class ModelBuilderExtensions
{
    public static void RemoveCascadeDeleteConvention(this ModelBuilder modelBuilder)
    {
        var allForeignKeys = modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(entity => entity.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
            .ToList();

        foreach (var foreignKey in allForeignKeys)
        {
            foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}