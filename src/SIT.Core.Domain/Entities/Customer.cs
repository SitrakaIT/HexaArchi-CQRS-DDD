using SIT.Core.Domain.Enums;

namespace SIT.Core.Domain.Entities;

public class Customer : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public CustomerType CustomerType { get; set; }

    public Customer(string name, CustomerType customerType)
    {
        Name = name;
        CustomerType = customerType;
    }
    
}