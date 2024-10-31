using SIT.Core.Domain.Entities;

namespace SIT.Core.Domain.Repositories.Customers;

public interface ICustomerReadOnlyRepository
{
    Task<List<Customer>> GetAllAsync();
}