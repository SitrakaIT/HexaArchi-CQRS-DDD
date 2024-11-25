using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using MediatR;
using SIT.Core.Application.Customers.Commands;
using SIT.Core.Application.Customers.Commands.Requests;
using SIT.Core.Application.Customers.Commands.Results;
using SIT.Core.Domain.Entities;
using SIT.Core.Domain.Repositories.Customers;
using SIT.Shared.Abstractions.Interfaces;

namespace SIT.Core.Application.Customers.Commands.UseCases;

public class CreateCustomerCommandUseCase(
    IValidator<CreateCustomerCommand> validator,
    ICustomerWriteOnlyRepository customerWriteOnlyRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateCustomerCommand, Result<CreatedCustomerResult>>
{
    public async Task<Result<CreatedCustomerResult>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Result<CreatedCustomerResult>.Invalid(validationResult.AsErrors());
        
        if (await customerWriteOnlyRepository.ExistsByNameAsync(request.Name))
            return Result<CreatedCustomerResult>.Error("The provided customer name already exists.");

        var customer = new Customer(request.Name, request.CustomerType);
        
        customerWriteOnlyRepository.Add(customer);

        await unitOfWork.SaveChangesAsync();

        return Result<CreatedCustomerResult>.Success(new CreatedCustomerResult(customer.Id),
            "New customer Successfully created!");
    }
}