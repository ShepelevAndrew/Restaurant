using System.Net;
using ErrorOr;

namespace Restaurant.Domain.Errors;

public static partial class Errors
{
    public static class Order
    {
        public static Error OrderNotFound => Error.NotFound(
            code: ((int)HttpStatusCode.NotFound).ToString(),
            description: "Order is not found.");
    }
}