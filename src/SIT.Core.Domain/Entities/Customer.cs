using SIT.Core.Domain.Enums;
using SIT.Shared.Abstractions;
using SIT.Shared.Abstractions.Interfaces;

namespace SIT.Core.Domain.Entities;

public class Customer : BaseEntity
{
    public string Name { get; }
    public CustomerType CustomerType { get; }

    public Customer(string name, CustomerType customerType)
    {
        Name = name;
        CustomerType = customerType;
    }
    
}