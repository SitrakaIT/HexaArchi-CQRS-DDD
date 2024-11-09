using System.Text.Json.Serialization;

namespace SIT.API.Models;

public sealed class ApiGenericResponse<T> : ApiResponse
{
    [JsonConstructor]
    public ApiGenericResponse(T result, bool success, string successMessage, int statusCode, IEnumerable<ApiErrorResponse> errors)
        : base(success, successMessage, statusCode, errors)
    {
        Result = result;
    }

    public ApiGenericResponse()
    {
    }

    public T Result { get; private init; }

    public static ApiGenericResponse<T> Ok(T result) =>
        new() { Success = true, StatusCode = StatusCodes.Status200OK, Result = result };

    public static ApiGenericResponse<T> Ok(T result, string successMessage) =>
        new() { Success = true, StatusCode = StatusCodes.Status200OK, Result = result, SuccessMessage = successMessage };
}