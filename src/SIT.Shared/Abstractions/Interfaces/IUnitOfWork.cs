namespace SIT.Shared.Abstractions.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task SaveChangesAsync();
}