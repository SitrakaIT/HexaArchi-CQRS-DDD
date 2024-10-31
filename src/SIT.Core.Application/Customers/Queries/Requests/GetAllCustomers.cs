using Ardalis.Result;
using MediatR;
using SIT.Core.Application.Customers.Queries.Results;

namespace SIT.Core.Application.Customers.Queries.Requests;

public class GetAllCustomers : IRequest<Result<IEnumerable<CustomerResult>>>
{
    
}