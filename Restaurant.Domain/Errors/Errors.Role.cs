using System.Net;
using ErrorOr;

namespace Restaurant.Domain.Errors;

public static partial class Errors
{
    public static class Role
    {
        public static Error RoleNotFound => Error.NotFound(
            code: ((int)HttpStatusCode.NotFound).ToString(),
            description: "Role is not found.");
    }
}