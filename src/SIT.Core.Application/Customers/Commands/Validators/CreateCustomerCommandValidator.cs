using FluentValidation;
using SIT.Core.Application.Customers.Commands.Requests;

namespace SIT.Core.Application.Customers.Commands.Validators;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.CustomerType).NotEmpty().NotNull();
    }
}