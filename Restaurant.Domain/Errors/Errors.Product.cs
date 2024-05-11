using System.Net;
using ErrorOr;

namespace Restaurant.Domain.Errors;

public static partial class Errors
{
    public static class Product
    {
        public static Error DuplicateAlias => Error.Conflict(
            code: ((int)HttpStatusCode.Conflict).ToString(),
            description: "Alias is already in use.");

        public static Error ProductNotFound => Error.NotFound(
            code: ((int)HttpStatusCode.NotFound).ToString(),
            description: "Product is not found.");
    }
}