using System.Net;
using ErrorOr;

namespace Restaurant.Domain.Errors;

public static partial class Errors
{ 
    public static int GetStatusCode(this Error error)
    {
        if (int.TryParse(error.Code, out var result))
        {
            return result;
        }

        var httpStatusCode = GetHttpStatusCodeByErrorType(error);
        return (int)httpStatusCode;
    }

    public static HttpStatusCode GetHttpStatusCode(this Error error)
    {
        if (int.TryParse(error.Code, out var result))
        {
            return (HttpStatusCode)result;
        }

        return GetHttpStatusCodeByErrorType(error);
    }

    private static HttpStatusCode GetHttpStatusCodeByErrorType(Error error)
    {
        return error.Type switch
        {
            ErrorType.Validation => HttpStatusCode.BadRequest,
            ErrorType.Conflict => HttpStatusCode.Conflict,
            ErrorType.NotFound => HttpStatusCode.NotFound,
            ErrorType.Unauthorized => HttpStatusCode.Unauthorized,
            ErrorType.Forbidden => HttpStatusCode.Forbidden,
            _ => HttpStatusCode.InternalServerError
        };
    }
}