using System.Net;
using ErrorOr;

namespace Restaurant.Domain.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(
            code: ((int)HttpStatusCode.Conflict).ToString(),
            description: "Email is already in use.");

        public static Error UserNotFound => Error.NotFound(
            code: ((int)HttpStatusCode.NotFound).ToString(),
            description: "User is not found.");
    }
}