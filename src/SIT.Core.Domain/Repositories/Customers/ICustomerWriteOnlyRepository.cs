using SIT.Core.Domain.Entities;
using SIT.Shared.Abstractions.Interfaces;

namespace SIT.Core.Domain.Repositories.Customers;

public interface ICustomerWriteOnlyRepository : IWriteOnlyRepository<Customer, int>
{
    Task<bool> ExistsByNameAsync(string name);
}