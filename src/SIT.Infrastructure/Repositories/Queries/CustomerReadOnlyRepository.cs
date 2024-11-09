using SIT.Core.Domain.Entities;
using SIT.Core.Domain.Repositories.Customers;

namespace SIT.Infrastructure.Repositories.Queries;

public class CustomerReadOnlyRepository : ICustomerReadOnlyRepository
{
    public Task<List<Customer>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}