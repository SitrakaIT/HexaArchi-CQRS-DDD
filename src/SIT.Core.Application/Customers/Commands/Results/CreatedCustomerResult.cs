namespace SIT.Core.Application.Customers.Commands.Results;

public class CreatedCustomerResult(int id)
{
    public int Id { get; set; } = id;
}