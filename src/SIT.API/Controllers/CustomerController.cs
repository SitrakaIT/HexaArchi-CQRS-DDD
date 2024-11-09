using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIT.API.Extensions;
using SIT.API.Models;
using SIT.Core.Application.Customers.Commands.Requests;
using SIT.Core.Application.Customers.Commands.Results;

namespace SIT.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiGenericResponse<CreatedCustomerResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand createCustomerCommand)
    {
        var commandResult = await mediator.Send(createCustomerCommand);
        
        return commandResult.ToActionResult();
    }
}