using Ardalis.Result;
using FluentValidation;
using MediatR;
using SIT.Core.Application.Customers.Commands;
using SIT.Core.Application.Customers.Commands.Requests;
using SIT.Core.Application.Customers.Commands.Results;
using SIT.Core.Domain.Entities;
using SIT.Core.Domain.Repositories.Customers;

namespace SIT.Core.Application.Customers.Commands.UseCases;

public class CreateCustomerCommandUseCase(
    IValidator<CreateCustomerCommand> validator,
    ICustomerWriteOnlyRepository customerWriteOnlyRepository) : IRequestHandler<CreateCustomerCommand, Result<CreatedCustomerResult>>
{
    public async Task<Result<CreatedCustomerResult>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        var customer = new Customer(request.Name, request.CustomerType);

        return Result<CreatedCustomerResult>.Success(new CreatedCustomerResult(customer.Id),
            "New customer Successfully created!");
    }
}