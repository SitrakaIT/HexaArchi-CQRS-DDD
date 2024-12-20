using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using SIT.API.Models;
using IResult = Ardalis.Result.IResult;

namespace SIT.API.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
        return result.IsSuccess
            ? new OkObjectResult(ApiGenericResponse<T>.Ok(result.Value, result.SuccessMessage))
            : result.ToHttpNonSuccessResult();
    }
    
    private static IActionResult ToHttpNonSuccessResult(this IResult result)
    {
        var errors = result.Errors.Select(error => new ApiErrorResponse(error)).ToList();

        switch (result.Status)
        {
            case ResultStatus.Invalid:

                var validationErrors = result
                    .ValidationErrors
                    .Select(validation => new ApiErrorResponse(validation.ErrorMessage));

                return new BadRequestObjectResult(ApiResponse.BadRequest(validationErrors));

            case ResultStatus.NotFound:
                return new NotFoundObjectResult(ApiResponse.NotFound(errors));

            case ResultStatus.Forbidden:
                return new ForbidResult();

            case ResultStatus.Unauthorized:
                return new UnauthorizedObjectResult(ApiResponse.Unauthorized(errors));

            default:
                return new BadRequestObjectResult(ApiResponse.BadRequest(errors));
        }
    }
}