using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SIT.Infrastructure.Contexts;
using SIT.Shared.Abstractions.Interfaces;

namespace SIT.Infrastructure.Repositories;

public sealed class UnitOfWork(
    WriteDbContext writeDbContext,
    ILogger<UnitOfWork> logger):  IUnitOfWork
{
    public async Task SaveChangesAsync()
    {
        var strategy = writeDbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction =
                await writeDbContext.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            
            logger.LogInformation("---- Begin transaction: '{TransactionId}'", transaction.TransactionId);

            try
            {
                var rowsAffected = await writeDbContext.SaveChangesAsync();
                
                logger.LogInformation("---- Commit transaction: '{TransactionId}'", transaction.TransactionId);
                
                await transaction.CommitAsync();
                
                logger.LogInformation("---- Transaction successfully confirmed: '{TransactionId}', Rows affected: {RowsAffected}",
                    transaction.TransactionId,
                    rowsAffected);
            }
            catch (Exception e)
            {
                logger.LogError(
                    e,
                    "An unexpected exception occured while committing the transaction: '{TransactionId}', message: {Message}",
                        transaction.TransactionId,
                        e.Message);
                
                await transaction.RollbackAsync();
                
                throw;
            }

        });
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    ~UnitOfWork() => Dispose(false);

    private bool _disposed;

    private void Dispose(bool disposing)
    {
        if (_disposed)
            return;
        
        if (disposing)
            writeDbContext.Dispose();
        
        _disposed = true;
    }
}