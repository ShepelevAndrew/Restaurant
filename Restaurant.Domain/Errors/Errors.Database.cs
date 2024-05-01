using System.Net;
using ErrorOr;

namespace Restaurant.Domain.Errors;

public static partial class Errors
{
    public static class Database
    {
        public static Error DatabaseFailure => Error.Failure(
            code: ((int)HttpStatusCode.InternalServerError).ToString(),
            description: "Problem with our database.");
    }
}