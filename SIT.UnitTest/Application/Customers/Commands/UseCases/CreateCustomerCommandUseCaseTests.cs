using Bogus;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using SIT.Core.Application.Customers.Commands.Requests;
using SIT.Core.Application.Customers.Commands.UseCases;
using SIT.Core.Application.Customers.Commands.Validators;
using SIT.Core.Domain.Enums;
using SIT.Core.Domain.Repositories.Customers;
using SIT.Infrastructure.Repositories;
using SIT.Infrastructure.Repositories.Commands;
using SIT.Shared.Abstractions.Interfaces;
using SIT.UnitTest.Fixtures;

namespace SIT.UnitTest.Application.Customers.Commands.UseCases;

public class CreateCustomerCommandUseCaseTests(TestDbContextSetup contextSetup) : IClassFixture<TestDbContextSetup>
{
    private readonly CreateCustomerCommandValidator _validator = new();

    [Fact]
    public async Task Create_ValidCustomer_ShouldReturnSuccessResult()
    {
        var customerCommand = new Faker<CreateCustomerCommand>()
            .RuleFor(command => command.Name, f => f.Name.JobArea())
            .RuleFor(command => command.CustomerType, f => f.PickRandom<CustomerType>())
            .Generate();

        var unitOfWork = new UnitOfWork(
            contextSetup.Context,
            Substitute.For<ILogger<UnitOfWork>>()
        );

        var handler = new CreateCustomerCommandUseCase(
            _validator,
            new CustomerWriteOnlyRepository(contextSetup.Context),
            unitOfWork
        );

        var execute = await handler.Handle(customerCommand, CancellationToken.None);

        execute.Should().NotBeNull();
        execute.IsSuccess.Should().BeTrue();
        execute.SuccessMessage.Should().Be("New customer Successfully created!");
        execute.Value.Should().NotBeNull();
        execute.Value.Id.Should().BeOfType(typeof(int));
    }

    [Fact]
    public async Task Create_InvalidCustomer_ShouldReturnErrorResult()
    {
        var handler = new CreateCustomerCommandUseCase(
            _validator,
            Substitute.For<ICustomerWriteOnlyRepository>(),
            Substitute.For<IUnitOfWork>()
        );
        
        var execute = await handler.Handle(new CreateCustomerCommand(), CancellationToken.None);
        
        execute.Should().NotBeNull();
        execute.IsSuccess.Should().BeFalse();
        execute.ValidationErrors.Should().HaveCount(2);
        execute.ValidationErrors.Should().Contain(x => x.ErrorCode.Equals("NotEmptyValidator"));
    }
}