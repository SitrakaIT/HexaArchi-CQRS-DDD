using Ardalis.Result;
using MediatR;
using SIT.Core.Application.Customers.Queries.Requests;
using SIT.Core.Application.Customers.Queries.Results;
using SIT.Core.Domain.Repositories.Customers;

namespace SIT.Core.Application.Customers.Queries.UseCases;

public class GetAllCustomersQueryUseCase(ICustomerReadOnlyRepository customerReadOnlyRepository) : IRequestHandler<GetAllCustomers, Result<IEnumerable<CustomerResult>>>
{
    public Task<Result<IEnumerable<CustomerResult>>> Handle(GetAllCustomers request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}