using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using MediatR;
using SIT.Core.Application.Customers.Commands.Results;
using SIT.Core.Domain.Enums;

namespace SIT.Core.Application.Customers.Commands.Requests;

public class CreateCustomerCommand : IRequest<Result<CreatedCustomerResult>>
{
    [Required]
    [DataType(DataType.Text)]
    public string Name { get; set; }
    
    [Required]
    public CustomerType CustomerType { get; set; }
}