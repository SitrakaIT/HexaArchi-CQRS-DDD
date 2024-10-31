using SIT.Core.Domain.Enums;

namespace SIT.Core.Application.Customers.Queries.Results;

public class CustomerResult
{
    public int Id { get; set; }
    public string Name { get; set; }
    public CustomerType CustomerType { get; set; }

    public CustomerResult(int id, string name, CustomerType customerType)
    {
        Id = id;
        Name = name;
        CustomerType = customerType;
    }
}