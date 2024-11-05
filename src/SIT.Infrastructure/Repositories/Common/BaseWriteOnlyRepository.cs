using Microsoft.EntityFrameworkCore;
using SIT.Infrastructure.Contexts;
using SIT.Shared.Abstractions.Interfaces;

namespace SIT.Infrastructure.Repositories.Common;

public abstract class BaseWriteOnlyRepository<TEntity, TKey>(WriteDbContext context) : IWriteOnlyRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    private static readonly Func<WriteDbContext, TKey, Task<TEntity>> GetByIdCompiledAsync =
        EF.CompileAsyncQuery((WriteDbContext wContext, TKey id) =>
            wContext
                .Set<TEntity>()
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefault(entity => entity.Id.Equals(id)));

    protected readonly WriteDbContext Context = context;
    
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
    
    public void Add(TEntity entity) => _dbSet.Add(entity);
    
    public void Update(TEntity entity) => _dbSet.Update(entity);
    
    public void Remove(TEntity entity) => _dbSet.Remove(entity);
    
    public async Task<TEntity> GetByIdAsync(TKey id) => await GetByIdCompiledAsync(Context, id);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    ~BaseWriteOnlyRepository() => Dispose(false);

    private bool _disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;
        
        if (disposing)
            Context.Dispose();
        
        _disposed = true;
    }
}