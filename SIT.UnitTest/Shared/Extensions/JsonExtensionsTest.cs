using FluentAssertions;
using SIT.Shared.Extensions;
using Xunit.Categories;

namespace SIT.UnitTest.Shared.Extensions;

[UnitTest]
public class JsonExtensionsTest
{
    private const string CustomerJson =
        "{\"name\":\"CAPG\",\"email\":\"capg.esn@hotmail.com\",\"customerType\":\"esn\"}";
    
    [Fact]
    public void Should_ReturnJsonString_When_Serializing()
    {
        var customer = new CustomerMock("CAPG", "capg.esn@hotmail.com", CustomerMockType.Esn);
        var execute = customer.ToJson();
        execute.Should().NotBeNullOrWhiteSpace().And.BeEquivalentTo(CustomerJson);
    }

    [Fact]
    public void Should_ReturnEntity_When_Deserializing()
    {
        var expected = new CustomerMock("CAPG", "capg.esn@hotmail.com", CustomerMockType.Esn);
        
        var execute = CustomerJson.FromJson<CustomerMock>();
        
        execute.Should().NotBeNull().And.BeEquivalentTo(expected);
        execute.Name.Should().NotBeNullOrWhiteSpace();
        execute.Email.Should().NotBeNullOrWhiteSpace();
        execute.CustomerType.Should().Be(CustomerMockType.Esn);
    }

    [Fact]
    public void Should_ReturnNull_When_Serializing_Null()
    {
        CustomerMock nullCustomer = null;
        
        var execute = nullCustomer.ToJson();
        
        execute.Should().BeNull();
    }

    [Fact]
    public void Should_ReturnNull_When_Deserializing_Null()
    {
        const string nullCustomerStringJson = null;
        
        var execute = nullCustomerStringJson.FromJson<CustomerMock>();
        
        execute.Should().BeNull();
    }

    private enum CustomerMockType
    {
        Esn = 0,
        Freelance = 1
    }

    private record CustomerMock(string Name, string Email, CustomerMockType CustomerType)
    {
        public string Name { get; } = Name;
        public string Email { get; } = Email;
        public CustomerMockType CustomerType { get; } = CustomerType;
    }
}