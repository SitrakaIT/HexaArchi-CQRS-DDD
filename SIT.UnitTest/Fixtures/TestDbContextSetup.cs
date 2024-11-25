using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SIT.Infrastructure.Contexts;

namespace SIT.UnitTest.Fixtures;

public class TestDbContextSetup : IAsyncLifetime, IDisposable
{
    private const string ConnectionString = "Data Source=:memory:";
    private readonly SqliteConnection _connection;

    public WriteDbContext Context { get; set; }
    
    public TestDbContextSetup()
    {
        _connection = new SqliteConnection(ConnectionString);
        _connection.Open();
        
        var builder = new DbContextOptionsBuilder<WriteDbContext>().UseSqlite(_connection);
        Context = new WriteDbContext(builder.Options);
    }


    public async Task InitializeAsync()
    {
        await Context.Database.EnsureDeletedAsync();
        await Context.Database.EnsureCreatedAsync();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    ~TestDbContextSetup() => Dispose(false);
    
    private bool _disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            _connection?.Dispose();
            Context?.Dispose();
        }
        
        _disposed = true;
    }
}